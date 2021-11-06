using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Domain.Models.Users;

namespace Heka.Domain.Models.Appointment
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        [AllowNull]
        public string Comment { get; set; }
        public bool IsDelete { get; set; }
        public AppointmentStatus Status { get; set; }
        public DateTime RequestedDate { get; set; }

        #region Navigarion

        public User User { get; set; }

        #endregion
    }

    public enum AppointmentStatus
    {
        [Display(Name = "inProgress")]
        InProgress,
        [Display(Name = "confirmed")]
        Confirmed,
        [Display(Name = "rejected")]
        Rejected
    }

}
