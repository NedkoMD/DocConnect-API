using DocConnect.Business.Models.DTOs.Speciality;
using DocConnect.Business.Models.Results;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Represents a service for managing specialities.
    /// </summary>
    public interface ISpecialityService
    {
        /// <summary>
        /// Adds a new speciality asynchronously.
        /// </summary>
        /// <param name="specialityAddDTO">The data transfer object containing information for the new speciality.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the created speciality view DTO.</returns>
        Task<IResult<SpecialityResultDTO>> AddAsync(SpecialityAddDTO specialityAddDTO);

        /// <summary>
        /// Deletes a speciality by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the speciality to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the deleted speciality view DTO.</returns>
        Task<IResult<SpecialityResultDTO>> DeleteAsync(uint id);

        /// <summary>
        /// Retrieves all specialities asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of speciality view DTOs.</returns>
        Task<IEnumerable<SpecialityResultDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a speciality by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the speciality to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the found speciality view DTO; otherwise, an appropriate error result.</returns>
        Task<IResult<SpecialityResultDTO>> GetByIdAsync(uint id);

        /// <summary>
        /// Updates an existing speciality asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the speciality to be updated.</param>
        /// <param name="specialityUpdateDTO">The data transfer object containing updated information for the speciality.</param>
        /// <returns>A task representing the asynchronous operation, returning a result containing the updated speciality view DTO.</returns>
        Task<IResult<SpecialityResultDTO>> UpdateAsync(uint id, SpecialityUpdateDTO specialityUpdateDTO);
    }
}