using DocConnect.Data.Models.Entities;
using DocConnect.Data.Repositories;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Represents a repository for managing appointments.
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Adds a new appointment asynchronously.
        /// </summary>
        /// <param name="appointment">The appointment object to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Appointment appointment);

        /// <summary>
        /// Deletes an appointment asynchronously.
        /// </summary>
        /// <param name="appointment">The appointment object to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Appointment appointment);

        /// <summary>
        /// Retrieves all appointments asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of appointments.</returns>
        Task<IEnumerable<Appointment>> GetAllAsync();

        /// <summary>
        /// Retrieves an appointment by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the appointment.</param>
        /// <returns>A task representing the asynchronous operation, returning the appointment object if found; otherwise, null.</returns>
        Task<Appointment> GetByIdAsync(long id);

        /// <summary>
        /// Updates an existing appointment asynchronously.
        /// </summary>
        /// <param name="appointment">The appointment object with updated information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Appointment appointment);

        /// <summary>
        /// Get all appointments for a doctor within a specified time range asynchronously.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <param name="start">The start date and time of the range.</param>
        /// <param name="end">The end date and time of the range.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of appointments.</returns>
        Task<IEnumerable<Appointment>> GetAllByDoctorIdAsync(uint doctorId, DateTime start, DateTime end);

        /// <summary>
        /// Get all appointments associated with a specific patient asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the patient.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of appointments.</returns>
        Task<IEnumerable<Appointment>> GetAllByPatientIdAsync(uint userId);

        /// <summary>
        /// Get an appointment by doctor and patient IDs asynchronously.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <param name="userId">The unique identifier of the patient.</param>
        /// <returns>A task representing the asynchronous operation, returning the appointment.</returns>
        Task<Appointment> GetByDoctorIdAndPatientIdAsync(uint doctorId, uint userId);

        /// <summary>
        /// Get all detailed appointments associated with a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed appointments.</returns>
        Task<IEnumerable<AppointmentDetailedModel>> GetAllDetailedAppointmentAsync(uint userId);

        /// <summary>
        /// Get all future detailed appointments associated with a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed appointments.</returns>
        Task<IEnumerable<AppointmentDetailedModel>> GetAllFutureAppoimtmentsByUserIdAsync(uint userId);

        /// <summary>
        /// Get all past detailed appointments associated with a specific user asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed appointments.</returns>
        Task<IEnumerable<AppointmentDetailedModel>> GetAllPastAppointmentsByUserIdAsync(uint userId);

        /// <summary>
        /// Get a detailed appointment by its unique identifier asynchronously.
        /// </summary>
        /// <param name="Id">The unique identifier of the appointment.</param>
        /// <returns>A task representing the asynchronous operation, returning the detailed appointment.</returns>
        Task<AppointmentDetailedModel> GetDetailedAppointmentByIdAsync(long Id);
    }

}
