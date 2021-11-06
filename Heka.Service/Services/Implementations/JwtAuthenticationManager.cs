using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Heka.Service.Services.DTOs.AuthenticationDTOs;
using Heka.Service.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Heka.Service.Services.Implementation
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        #region Dependancy Injection

        private readonly IUserService _userService;

        public JwtAuthenticationManager(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        public async Task<LoginDto> JwtAuthentication(UserCredentialDto userCredential)
        {

            var user = await _userService.LoginUser(userCredential);

            if (user is null)
                return new LoginDto
                {
                    Message = "Username or password is incorrect"
                };

            var _key = "This is jwtKey for test";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);



            #region Claims

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim("Role",user.RoleTitle)
            };
            

            #endregion



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return new LoginDto
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
                RoleTitle = user.RoleTitle,
                Message = "Authentication has been done successfully"
            };
        }
    }
}
