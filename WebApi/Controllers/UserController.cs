using Application.DTOs;
using Application.InterfacesService;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(registerDto);

            if (result == null)
                return BadRequest("Falha ao registrar o usuário");

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }
    }
}
