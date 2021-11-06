using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Heka.DataAccess.Repositories.Interfaces;
using Heka.Domain.Models.Appointment;
using Heka.Service.Services.DTOs;
using Heka.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Heka.Service.Services.Helpers.ExtentionMethods;
namespace Heka.Service.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {

        #region Dependancy Injection

        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        #endregion


        public async Task<FullAppointmentDto> SetAppointment(AppointmentDto appointment, int userId)
        {

            if (await IsAppointmentSetBefore(appointment.Time, appointment.Date))
                return new FullAppointmentDto
                {
                    IsDone = "False",
                    Message = "There is no free time on the selected date and time"
                };

            var newAppointment = new Appointment()
            {
                Comment = appointment.Comment,
                Date = appointment.Date,
                IsDelete = false,
                Status = AppointmentStatus.InProgress,
                RequestedDate = DateTime.Now,
                Time = appointment.Time,
                UserId = userId
            };

            var addedResult = await _appointmentRepository.AddAppointment(newAppointment);
            await _appointmentRepository.SaveChanges();

            if (addedResult is false)
                return null;

            return new FullAppointmentDto
            {
                IsDelete = newAppointment.IsDelete,
                Comment = newAppointment.Comment,
                Date = newAppointment.Date,
                Time = newAppointment.Time,
                Status = newAppointment.Status.GetEnumDisplayName(),
                RequestedDate = newAppointment.RequestedDate,
                Message = "Your appointment request has been sent, wait for the confirmation",
                IsDone = "True"
            };
        }

        //This method shows if there is an appointment at the same hour and date or not
        public async Task<bool> IsAppointmentSetBefore(string time, string date)
        {
            var hour = time.Split(':')[0]; //It gets the full time and retrieves only hour
            return await _appointmentRepository.IsAppointmentSetBefore(hour, date);
        }

        public async Task<FullAppointmentDto> GetAppointmentByAppointmentId(int appointmentId)
        {
            var appointmet = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);

            if (appointmet is null)
                return null;

            return new FullAppointmentDto
            {
                IsDelete = appointmet.IsDelete,
                Comment = appointmet.Comment,
                Date = appointmet.Date,
                Time = appointmet.Time,
                AppointmentId = appointmet.AppointmentId,
                Status = appointmet.Status.GetEnumDisplayName(),
                RequestedDate = appointmet.RequestedDate,
                UserId = appointmet.UserId
            };
        }

        public async Task<List<FullAppointmentDto>> GetAllAppointments()
        {
            var allAppointments = await _appointmentRepository.GetAllAppointments();

            return allAppointments.Select(a => new FullAppointmentDto
            {
                IsDelete = a.IsDelete,
                Comment = a.Comment,
                Date = a.Date,
                Time = a.Time,
                AppointmentId = a.AppointmentId,
                RequestedDate = a.RequestedDate,
                Status = a.Status.GetEnumDisplayName(),
                UserId = a.UserId,
                Username = a.User.Username
            }).ToList();
        }

        public async Task<List<FullAppointmentDto>> GetAllAppointmentsByUserId(int userId)
        {
            var allAppointments = await _appointmentRepository.GetAllAppointmentsByUserId(userId);

            if (allAppointments is null)
                return null;

            return allAppointments.Select(a => new FullAppointmentDto()
            {
                IsDelete = a.IsDelete,
                Comment = a.Comment,
                Date = a.Date,
                Time = a.Time,
                AppointmentId = a.AppointmentId,
                RequestedDate = a.RequestedDate,
                Status = a.Status.GetEnumDisplayName(),
                UserId = a.UserId,
                Username = a.User.Username
            }).ToList();
        }

        public async Task<FullAppointmentDto> ConfirmAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);
            appointment.Status = AppointmentStatus.Confirmed;
            await _appointmentRepository.EditAppointment(appointment);
            return new FullAppointmentDto
            {
                IsDelete = appointment.IsDelete,
                Comment = appointment.Comment,
                Date = appointment.Date,
                Time = appointment.Time,
                AppointmentId = appointment.AppointmentId,
                RequestedDate = appointment.RequestedDate,
                Status = appointment.Status.GetEnumDisplayName(),
                UserId = appointment.UserId,
                IsDone = "True",
                Message = "Appointment has been confirmed"
            };
        }

        public async Task<FullAppointmentDto> CancelAppointment(int appointmentId,int userId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);

            if (appointment.UserId != userId)
                return new FullAppointmentDto { IsDone = "False",Message = "You can only delete your appointment request !"};

            appointment.IsDelete = true;

            await _appointmentRepository.EditAppointment(appointment);
            return new FullAppointmentDto
            {
                IsDelete = appointment.IsDelete,
                Comment = appointment.Comment,
                Date = appointment.Date,
                Time = appointment.Time,
                AppointmentId = appointment.AppointmentId,
                RequestedDate = appointment.RequestedDate,
                Status = appointment.Status.GetEnumDisplayName(),
                UserId = appointment.UserId,
                IsDone = "True",
                Message = "Appointment has been cancelled"
            };
        }

        public async Task<FullAppointmentDto> RejectAppointment(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);

            appointment.Status = AppointmentStatus.Rejected;

            await _appointmentRepository.EditAppointment(appointment);
            return new FullAppointmentDto
            {
                IsDelete = appointment.IsDelete,
                Comment = appointment.Comment,
                Date = appointment.Date,
                Time = appointment.Time,
                AppointmentId = appointment.AppointmentId,
                RequestedDate = appointment.RequestedDate,
                Status = appointment.Status.GetEnumDisplayName(),
                UserId = appointment.UserId,
                IsDone = "True",
                Message = "Appointment has been rejected"
            };
        }
    }
}
