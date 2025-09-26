using Microsoft.AspNetCore.Mvc;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Infrastructure.Services;

namespace ContactFundApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(AuthRequestDto request)
    {
        if (request.Username == "test" && request.Password == "1234")
        {
            var token = _jwtTokenService.GenerateToken(request.Username);
            var response = new AuthResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            return Ok(response);
        }

        return Unauthorized(new { success = false, message = "Invalid credentials" });
    }
}
