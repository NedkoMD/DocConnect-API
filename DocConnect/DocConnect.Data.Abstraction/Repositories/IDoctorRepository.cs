using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Models;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Represents a repository for managing doctors.
    /// </summary>
    public interface IDoctorRepository
    {
        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor object to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Doctor doctor);

        /// <summary>
        /// Deletes a doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor object to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Doctor doctor);

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of doctors.</returns>
        Task<IEnumerable<Doctor>> GetAllAsync(int takeAmount, int page);

        /// <summary>
        /// Retrieves a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>A task representing the asynchronous operation, returning the doctor object if found; otherwise, null.</returns>
        Task<Doctor> GetByIdAsync(uint id);

        /// <summary>
        /// Updates an existing doctor asynchronously.
        /// </summary>
        /// <param name="doctor">The doctor object with updated information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Doctor doctor);

        /// <summary>
        /// Get detailed doctor information by ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor.</param>
        /// <returns>A task representing the asynchronous operation, returning detailed doctor information.</returns>
        Task<DetailedDoctorInfo> GetDetailedDoctorInfoByIdAsync(uint id);

        /// <summary>
        /// Get a collection of doctor search models asynchronously based on various search criteria.
        /// </summary>
        /// <param name="name">The name of the doctor to search for (optional).</param>
        /// <param name="specialityName">The name of the doctor's specialty to search for (optional).</param>
        /// <param name="cityName">The name of the city where the doctor practices (optional).</param>
        /// <param name="takeAmount">The number of search results to retrieve.</param>
        /// <param name="skipAmount">The number of search results to skip.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of doctor search models.</returns>
        Task<IEnumerable<DoctorSearchModel>> GetAllDoctorSearchModelsAsync(string name, string specialityName, string cityName, int takeAmount, int skipAmount);

        Task<uint> GetDoctorIdBySpecialistId(uint specialistId);
    }
}
