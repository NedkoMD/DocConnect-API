using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Represents a service for managing appointments.
    /// </summary>
    public interface IAppointmentService
    {
        /// <summary>
        /// Adds a new appointment asynchronously.
        /// </summary>
        /// <param name="appointmentAddDTO">The data transfer object containing information for the new appointment.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the created appointment view DTO.</returns>
        Task<IResult<AppointmentResultDTO>> AddAsync(AppointmentAddDTO appointmentAddDTO);

        /// <summary>
        /// Deletes an appointment asynchronously based on its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing detailed information about the deleted appointment, if successful.</returns>
        Task<IResult<AppointmentDetailedEmailDTO>> DeleteAsync(long id);

        /// <summary>
        /// Retrieves all appointments asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of appointment view DTOs.</returns>
        Task<IEnumerable<AppointmentResultDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves an appointment by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the found appointment view DTO; otherwise, an appropriate error result.</returns>
        Task<IResult<AppointmentResultDTO>> GetByIdAsync(long id);

        /// <summary>
        /// Updates an existing appointment asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment to be updated.</param>
        /// <param name="appointmentUpdateDTO">The data transfer object containing updated information for the appointment.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the updated appointment view DTO.</returns>
        Task<IResult<AppointmentResultDTO>> UpdateAsync(long id, AppointmentUpdateDTO appointmentUpdateDTO);

        /// <summary>
        /// Retrieves all appointments associated with a specific doctor asynchronously.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor whose appointments are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of appointment view DTOs.</returns>
        Task<AppointmentDoctorResultDTO> GetAllByDoctorIdAsync(uint doctorId);

        /// <summary>
        /// Retrieves all appointments associated with a specific patient asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the patient whose appointments are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of appointment view DTOs.</returns>
        Task<IEnumerable<AppointmentResultDTO>> GetAllByPatientIdAsync(uint userId);

        /// <summary>
        /// Retrieves all detailed appointments associated with a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose detailed appointments are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed appointment result DTOs.</returns>
        Task<IEnumerable<AppointmentDetailedResultDTO>> GetAllDetailedAppointmentsAsync(uint userId);

        /// <summary>
        /// Retrieves all future detailed appointments associated with a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose future detailed appointments are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed appointment result DTOs.</returns>
        Task<IEnumerable<AppointmentDetailedResultDTO>> GetAllFutureAppointmentsByUserIdAsync(uint userId);

        /// <summary>
        /// Retrieves all past detailed appointments associated with a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose past detailed appointments are to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed appointment result DTOs.</returns>
        Task<IEnumerable<AppointmentDetailedResultDTO>> GetAllPastAppointmentsByUserIdAsync(uint userId);
    }
}
