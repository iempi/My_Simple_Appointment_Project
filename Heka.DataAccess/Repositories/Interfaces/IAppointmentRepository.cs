using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Domain.Models.Appointment;

namespace Heka.DataAccess.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> AddAppointment(Appointment appointment);
        Task<bool> IsAppointmentSetBefore(string hour, string date);
        Task<Appointment> GetAppointmentByAppointmentId(int appointmentId);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetAllAppointmentsByUserId(int userId);
        Task<Appointment> EditAppointment(Appointment appointment);
        Task SaveChanges();
    }
}
