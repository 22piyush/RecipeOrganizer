using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RecipeOrganizer.Domain.Entity;
using RecipeOrganizer.Domain.Services;
using RecipeOrganizer.Infrastructure.Query;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace RecipeOrganizer.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public AuthService( IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("RecipeOrganizerDB");
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        RegisterResponse response = new RegisterResponse();

        SQLHelper sqlHelper = new SQLHelper();
        AuthQueryGenerator queryGenerator = new AuthQueryGenerator();

        try
        {
            string emailQuery = queryGenerator.GetUserByEmailQuery(request.Email);
            int emailCount = sqlHelper.ExecuteScalar(emailQuery, _connectionString);

            if (emailCount > 0)
            {
                response.ResponseCode = 400;
                response.ResponseMessage = "Email already exists";
                return response;
            }

            string userNameQuery = queryGenerator.GetUserByUserNameQuery(request.UserName);
            int userCount = sqlHelper.ExecuteScalar(userNameQuery, _connectionString);

            if (userCount > 0)
            {
                response.ResponseCode = 400;
                response.ResponseMessage = "Username already exists";
                return response;
            }

            User user = MapToUser(request);

            string insertUserQuery = queryGenerator.InsertUserQuery(user);
            int rowsAffected = sqlHelper.ExecuteNonQuery(insertUserQuery, _connectionString);

            if (rowsAffected <= 0)
            {
                response.ResponseCode = 500;
                response.ResponseMessage = "Failed to register user.";
                return response;
            }

            AssignUserRole(user.Id, "User", sqlHelper);

            response.UserId = user.Id;
            response.ResponseCode = 200;
            response.ResponseMessage = "Registration Successful";
        }
        catch (Exception ex)
        {
            response.ResponseCode = 500;
            response.ResponseMessage = "Internal Server Error";
        }
        finally
        {
            sqlHelper.CloseSqlConnection();
        }

        return response;
    }
    private User MapToUser(RegisterRequest request)
    {
        return new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
    }
    private void AssignUserRole(string userId, string roleName, SQLHelper sqlHelper)
    {
        string roleId = string.Empty;
        if (string.IsNullOrEmpty(userId))
            return;
        AuthQueryGenerator queryGenerator = new AuthQueryGenerator();

        string roleQuery = queryGenerator.GetRoleIdByNameQuery(roleName);

        using (MySqlDataReader reader = sqlHelper.ExecuteQuery(roleQuery, _connectionString))
        {
            if (reader.Read())
            {
                roleId = SQLHelper.GetStringValue(reader, "Id");
            }
        }

        if (!string.IsNullOrEmpty(roleId))
        {
            string roleAssignQuery = queryGenerator.AssignRoleQuery(userId, roleId);

            sqlHelper.ExecuteNonQuery(roleAssignQuery, _connectionString);
        }
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        LoginResponse response = new();
        SQLHelper sqlHelper = new SQLHelper();
        AuthQueryGenerator queryGenerator = new AuthQueryGenerator();

        try
        {
            string query = queryGenerator.GetUserForLogin(request.UserNameOrEmail);

            User user = null;

            string role = string.Empty;

            using (MySqlDataReader reader = sqlHelper.ExecuteQuery(query, _connectionString))
            {
                if (reader.Read())
                {
                    user = new User
                    {
                        Id = SQLHelper.GetStringValue(reader, "Id"),
                        UserName = SQLHelper.GetStringValue(reader, "UserName"),
                        Email = SQLHelper.GetStringValue(reader, "Email"),
                        PasswordHash = SQLHelper.GetStringValue(reader, "PasswordHash")
                    };

                    role = SQLHelper.GetStringValue(reader, "Role");
                }
            }

            if (user == null)
            {
                response.ResponseCode = 401;
                response.ResponseMessage = "Invalid Username/Email";
                return response;
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValidPassword)
            {
                response.ResponseCode = 401;
                response.ResponseMessage = "Invalid Password";
                return response;
            }

            string token = GenerateToken(user, role);

            response.ResponseCode = 200;
            response.ResponseMessage = "Login Successful";
            response.UserId = user.Id;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.Role = role;
            response.Token = token;

            return response;
        }
        catch (Exception ex)
        {
            response.ResponseCode = 500;
            response.ResponseMessage = ex.Message;

            return response;
        }
    }
    private string GenerateToken(User user, string role)
    {
        var claims = new[]{
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddHours(8), signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


