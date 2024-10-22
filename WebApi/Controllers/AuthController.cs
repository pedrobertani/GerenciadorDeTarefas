using Application.InterfacesService;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var usuario = await _authService.Login(login.Username, login.Password);
        if (usuario == null)
        {
            return Unauthorized();
        }

        var token = _authService.GenerateJwtToken(usuario);
        return Ok(new { Token = token, userName = usuario.UserName });
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult GetProtectedData()
    {
        return Ok(new { Message = "This is a protected endpoint", User = User.Identity.Name });
    }
}
