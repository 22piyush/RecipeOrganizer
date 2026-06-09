using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RecipeOrganizer.Domain.Entity;
using RecipeOrganizer.Domain.Services;
using RecipeOrganizer.Infrastructure.Query;

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
}
