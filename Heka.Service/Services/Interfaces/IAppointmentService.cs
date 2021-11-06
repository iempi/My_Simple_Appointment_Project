using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Service.Services.DTOs;

namespace Heka.Service.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<FullAppointmentDto> SetAppointment(AppointmentDto appointment,int userId);
        Task<bool> IsAppointmentSetBefore(string time, string date);
        Task<FullAppointmentDto> GetAppointmentByAppointmentId(int appointmentId);
        Task<List<FullAppointmentDto>> GetAllAppointments();
        Task<FullAppointmentDto> ConfirmAppointment(int appointmentId);
        Task<List<FullAppointmentDto>> GetAllAppointmentsByUserId(int userId);
        Task<FullAppointmentDto> CancelAppointment(int appointmentId,int userId);
        Task<FullAppointmentDto> RejectAppointment(int appointmentId);
    }
}
