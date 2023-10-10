using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.Enums;
using DocConnect.Presentation.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;

        public AppointmentController(IAppointmentService appointmentService,
                                     IEmailService emailService)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
        }

        // GET: api/Appointment
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _appointmentService.GetAllAsync();

            return Ok(response);
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] uint id)
        {
            var response = await _appointmentService.GetByIdAsync(id);

            return this.HandleResponse(response);
        }

        // GET: api/Appointment/doctor/5
        [HttpGet("doctor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllByDoctorIdAsync([FromRoute] uint id)
        {
            var response = await _appointmentService.GetAllByDoctorIdAsync(id);

            return Ok(response);
        }

        // GET: api/Appointment/patient/5
        [HttpGet("futute-patient-appointments/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllFutureAppointmentsByUserIdAsync([FromRoute] uint userId)
        {
            var response = await _appointmentService.GetAllFutureAppointmentsByUserIdAsync(userId);

            return Ok(response);
        }

        // GET: api/Appointment/past-patient-appointments/5
        [HttpGet("past-patient-appointments/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllPastAppointmentsByUserIdAsync([FromRoute] uint userId)
        {
            var response = await _appointmentService.GetAllPastAppointmentsByUserIdAsync(userId);

            return Ok(response);
        }

        // POST: api/Appointment
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] AppointmentAddDTO request)
        {
            var response = await _appointmentService.AddAsync(request);

            return this.HandleResponse(response);
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] uint id, [FromBody] AppointmentUpdateDTO request)
        {
            var response = await _appointmentService.UpdateAsync(id, request);

            return this.HandleResponse(response);
        }

        // DELETE: api/Appointment/5/
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] uint id)
        {
            var response = await _appointmentService.DeleteAsync(id);

            if(response.StatusCode == DocConnectStatusCode.NotFound)
            {
                return this.HandleResponse(response);
            }

            var emailResponse = await _emailService.SendAppointmentCancellationAsync(response.Data);

            return this.HandleResponse(emailResponse);
        }

        [HttpGet("get-detailed-appointment/{userId}")]
        public async Task<IActionResult> GetAllDetailedAppointmentsAsync(uint userId)
        {
            var response = await _appointmentService.GetAllDetailedAppointmentsAsync(userId);

            return Ok(response);
        }
    }
}
