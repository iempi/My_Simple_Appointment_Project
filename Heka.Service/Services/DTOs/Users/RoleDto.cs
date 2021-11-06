using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heka.Service.Services.DTOs.Users
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
        public List<UserDto> Users { get; set; }
    }
}
