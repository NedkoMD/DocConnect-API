using DocConnect.Business.Models.DTOs.Doctor;
using DocConnect.Business.Models.Results;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Represents a service for managing doctors.
    /// </summary>
    public interface IDoctorService
    {
        /// <summary>
        /// Adds a new doctor asynchronously.
        /// </summary>
        /// <param name="doctorAddDTO">The data transfer object containing information for the new doctor.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the created doctor view DTO.</returns>
        Task<IResult<DoctorResultDTO>> AddAsync(DoctorAddDTO doctorAddDTO);

        /// <summary>
        /// Deletes a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the deleted doctor view DTO.</returns>
        Task<IResult<DoctorResultDTO>> DeleteAsync(uint id);

        /// <summary>
        /// Retrieves all doctors asynchronously.
        /// </summary>
        /// <param name="takeAmount">Doctors to be requested.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of doctor view DTOs.</returns>
        Task<IEnumerable<DoctorResultDTO>> GetAllAsync(int takeAmount, int page);

        /// <summary>
        /// Retrieves a doctor by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the found doctor view DTO; otherwise, an appropriate error result.</returns>
        Task<IResult<DoctorResultDTO>> GetByIdAsync(uint id);

        /// <summary>
        /// Updates an existing doctor asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to be updated.</param>
        /// <param name="doctorUpdateDTO">The data transfer object containing updated information for the doctor.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the updated doctor view DTO.</returns>
        Task<IResult<DoctorResultDTO>> UpdateAsync(uint id, DoctorUpdateDTO doctorUpdateDTO);

        /// <summary>
        /// Retrieves detailed information about a doctor asynchronously based on their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor for whom detailed information is requested.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a result containing detailed information
        /// about the specified doctor if found.
        /// </returns>
        Task<IResult<DetailedDoctorInfoResultDTO>> GetDetailedDoctorInfoByIdAsync(uint id);

        /// <summary>
        /// Retrieves a collection of doctor search results asynchronously based on various search criteria.
        /// </summary>
        /// <param name="name">The name of the doctor to search for (optional).</param>
        /// <param name="specialityName">The name of the doctor's specialty to search for (optional).</param>
        /// <param name="locationName">The name of the doctor's location or clinic to search for (optional).</param>
        /// <param name="amount">The number of search results to retrieve per page.</param>
        /// <param name="page">The page number indicating which set of search results to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a collection of DoctorSearchResultDTOs
        /// containing search results based on the specified criteria.
        /// </returns>
        Task<IEnumerable<DoctorSearchResultDTO>> GetAllDoctorSearchModelsAsync(string name, string specialityName, string locationName, int amount, int page);

        Task<uint> GetDoctorIdBySpecialistId(uint specialistId);
    }
}
