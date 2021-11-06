using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.DataAccess.Context;
using Heka.DataAccess.Repositories.Interfaces;
using Heka.Domain.Models.Appointment;
using Microsoft.EntityFrameworkCore;

namespace Heka.DataAccess.Repositories.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {

        #region Dependancy Injection

        private readonly HekaDbContext _context;

        public AppointmentRepository(HekaDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> AddAppointment(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return true;
        }

        public async Task<bool> IsAppointmentSetBefore(string hour, string date)
        {
            return await _context.Appointments.AnyAsync(a => a.Time.StartsWith(hour) && a.Date == date);
        }

        public async Task<List<Appointment>> GetAllAppointmentsByUserId(int userId)
        {
            return await _context.Appointments
                .Include(a => a.User)
                .ThenInclude(u => u.Role)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByAppointmentId(int appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.User)
                .ThenInclude(u => u.Role)
                .SingleOrDefaultAsync(a => a.AppointmentId == appointmentId);
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments
                .Include(a=>a.User)
                .ThenInclude(u=>u.Role)
                .ToListAsync();
        }

        public async Task<Appointment> EditAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await SaveChanges();
            return await GetAppointmentByAppointmentId(appointment.AppointmentId);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
