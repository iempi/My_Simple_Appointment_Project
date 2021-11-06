using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heka.Service.Services.DTOs.Users
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RoleTitle { get; set; }
        public int RoleId { get; set; }
    }
}
