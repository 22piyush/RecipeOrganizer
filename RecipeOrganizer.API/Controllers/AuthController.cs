using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Domain.Entity;
using RecipeOrganizer.Domain.Services;

namespace RecipeOrganizer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ValidateRegisterRequest(request))
                {
                    return BadRequest(new
                    {
                        Success = false
                    });
                }

                RegisterResponse response = await _authService.RegisterAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        private bool ValidateRegisterRequest(RegisterRequest request)
        {

            if (request == null)
                return false;

            if (string.IsNullOrWhiteSpace(request.FirstName) || 
                string.IsNullOrWhiteSpace(request.LastName) || 
                string.IsNullOrWhiteSpace(request.UserName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
                return false;

            return true;
        }
    }
}
