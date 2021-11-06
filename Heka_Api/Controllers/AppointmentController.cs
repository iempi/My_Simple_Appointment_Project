using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Heka.Api.Helpers.Attributes;
using Heka.Service.Services.DTOs;
using Heka.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Heka.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        #region Dependancy Injection

        private readonly IAppointmentService _appointmenrService;

        public AppointmentController(IAppointmentService appointmenrService)
        {
            _appointmenrService = appointmenrService;
        }

        #endregion

        [HttpPost]
        [Route("SetAppointment")]
        public async Task<IActionResult> SetAppointment(AppointmentDto appointment)
        {

            if (!ModelState.IsValid)
                return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var settingAppointmentResult = await _appointmenrService.SetAppointment(appointment, userId);

            if (settingAppointmentResult.IsDone == "False")
                return Ok(new { message = settingAppointmentResult.Message, isDone = settingAppointmentResult.IsDone });

            return Ok(new { message = settingAppointmentResult.Message, isDone = settingAppointmentResult.IsDone });
        }

        [HttpPost]
        [Route("DeleteAppointmentRequest/{id}")]
        public async Task<IActionResult> DeleteAppointmentRequest(int id)
        {

            if (!ModelState.IsValid)
                return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var appointmentCancelationResult = await _appointmenrService.CancelAppointment(id, userId);

            if (appointmentCancelationResult.IsDone == "False")
                return Ok(new { message = appointmentCancelationResult.Message, isDone = appointmentCancelationResult.IsDone });

            return Ok(new { message = appointmentCancelationResult.Message, isDone = appointmentCancelationResult.IsDone });
        }

        [HttpPost]
        [Route("ConfirmRequest/{id}")]
        [AdminAuthorization]
        public async Task<IActionResult> ConfirmRequest(int id)
        {

            if (!ModelState.IsValid)
                return NotFound();

            var appointmentCancelationResult = await _appointmenrService.ConfirmAppointment(id);

            if (appointmentCancelationResult.IsDone == "False")
                return Ok(new { message = appointmentCancelationResult.Message, isDone = appointmentCancelationResult.IsDone });

            return Ok(new { message = appointmentCancelationResult.Message, isDone = appointmentCancelationResult.IsDone });
        }

        [HttpPost]
        [Route("RejectRequest/{id}")]
        [AdminAuthorization]
        public async Task<IActionResult> RejectRequest(int id)
        {

            if (!ModelState.IsValid)
                return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var appointmentCancelationResult = await _appointmenrService.RejectAppointment(id);

            if (appointmentCancelationResult.IsDone == "False")
                return Ok(new { message = appointmentCancelationResult.Message, isDone = appointmentCancelationResult.IsDone });

            return Ok(new { message = appointmentCancelationResult.Message, isDone = appointmentCancelationResult.IsDone });
        }

        [HttpGet]
        [Route("GetUserAppointments")]
        public async Task<IActionResult> GetUserAppointments()
        {

            if (!ModelState.IsValid)
                return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var userAppointments = await _appointmenrService.GetAllAppointmentsByUserId(userId);

            if (userAppointments is null)
                return Ok("No Appointments");

            return Ok(userAppointments);
        }

        [HttpGet]
        [Route("GetAllAppointments")]
        [AdminAuthorization]
        public async Task<IActionResult> GetAllAppointments()
        {

            if (!ModelState.IsValid)
                return NotFound();

            var userAppointments = await _appointmenrService.GetAllAppointments();

            if (userAppointments is null)
                return Ok("No Appointments");

            return Ok(userAppointments);
        }


    }
}
