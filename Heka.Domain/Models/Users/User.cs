using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heka.Domain.Models.Users
{
    public class User
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        public DateTime RegistrationDate { get; set; }

        #region Navigations

        public Role Role { get; set; }
        public ICollection<Appointment.Appointment> Appointments { get; set; }

        #endregion

    }
}
