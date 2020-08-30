using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CRUD.Auth;
using CRUD.DTO;
using CRUD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        public AuthenticationController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet("current")]
        public ActionResult GetCurrent()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var username = User.FindFirst(ClaimTypes.Name).Value;

            var role = User.FindFirst(ClaimTypes.Role).Value;

            return Ok(new { userId = userId, username = username, role = role });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreateDto userDto)
        {

            var res = await _repository.Register(new User { Username = userDto.Username, RoleId = userDto.RoleId },
                                                 userDto.Password);
            var createdUser = res.Data;
            if (!res.Success) return BadRequest(new { error = res.Message });
            return Ok(new { message = res.Message });
        }


        [Authorize]
        [HttpPost("password/update")]
        public async Task<ActionResult> ChangePassord(ChangePasswordDto passwordChangeDto)
        {
            var res = await _repository.ChangePassword(passwordChangeDto.Username, passwordChangeDto.CurrentPassword, passwordChangeDto.NewPassword);
            if (!res.Success) return BadRequest(new { error = res.Message });
            return Ok(new { message = res.Message });
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult> Login(UserLoginDto userDto)
        {
            var res = await _repository.Login(userDto.Username, userDto.Password);
            if (!res.Success) return BadRequest(new { error = res.Message });
            return Ok(res);
        }

    }
}
