using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heka.Domain.Models.Users
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }

        #region Navigations

        public List<User> Users { get; set; }

        #endregion

    }
}
