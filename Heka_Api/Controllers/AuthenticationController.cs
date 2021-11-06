using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Heka.Service.Services.DTOs.AuthenticationDTOs;
using Heka.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Heka.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Dependancy Injection

        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IUserService _userService;

        public AuthenticationController(IJwtAuthenticationManager jwtAuthenticationManager, IUserService userService)
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userService = userService;
        }

        #endregion

        
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserCredentialDto userCredential)
        {

            if (!ModelState.IsValid)
                return NotFound();

            var loginResult =
                await _jwtAuthenticationManager.JwtAuthentication(userCredential);
            

            if (loginResult.User is null)
                return Ok(new{Role = "",Token = loginResult.Message });

            return Ok(new { Role = loginResult.RoleTitle, Token = "Bearer " + loginResult.Token });
        }


        [HttpGet]
        [Route("IsAdmin")]
        public bool IsAdmin()
        {
            var isAdmin =(User.FindFirstValue("Role") == "admin")?true:false;

            return isAdmin;
        }


        //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //User.FindFirstValue("RoleId").ToString() != "1";



    }
}
