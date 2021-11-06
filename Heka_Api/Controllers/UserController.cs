using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heka.Api.Helpers.Attributes;
using Heka.Service.Services.DTOs;
using Heka.Service.Services.DTOs.AuthenticationDTOs;
using Heka.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Heka.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserController(IUserService userService, IJwtAuthenticationManager jwtAuthentication)
        {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthentication;
        }

        [HttpPost]
        [Route("AdminPanel")]
        [AdminAuthorization]
        public IActionResult AdminPanel()
        {
            return Ok();
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegistrationDto registration)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var registeredUser = await _userService.RegisterUser(registration);

            if (registeredUser is null)
                return Ok("DuplicatedUserName");

            var userCredential = new UserCredentialDto
                {Password = registration.Password, UserName = registration.Username};

            var token =
                await _jwtAuthenticationManager.JwtAuthentication(userCredential);

            return Ok("Bearer "+token.Token);
        }
        

    }
}
