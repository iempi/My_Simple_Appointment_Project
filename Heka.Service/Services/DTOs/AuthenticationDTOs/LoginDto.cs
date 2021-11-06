using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Service.Services.DTOs.Users;

namespace Heka.Service.Services.DTOs.AuthenticationDTOs
{
    public class LoginDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public string RoleTitle { get; set; }
        public string Message { get; set; }
    }
}
