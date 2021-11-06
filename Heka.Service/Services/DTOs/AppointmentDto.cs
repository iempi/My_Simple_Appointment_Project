using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Domain.Models.Appointment;

namespace Heka.Service.Services.DTOs
{
    public class AppointmentDto
    {
        public string Time { get; set; }
        public string Date { get; set; }
        [AllowNull]
        public string Comment { get; set; }
    }

    public class FullAppointmentDto
    {
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public string Time { get; set; }
        public string Username { get; set; }
        public string Date { get; set; }
        [AllowNull]
        public string Comment { get; set; }
        public bool IsDelete { get; set; }
        public string Status { get; set; }
        public DateTime RequestedDate { get; set; }
        [AllowNull]
        public string Message { get; set; }

        public string IsDone { get; set; }
    }

}
