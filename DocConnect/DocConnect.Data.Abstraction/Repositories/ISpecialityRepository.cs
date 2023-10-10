using DocConnect.Data.Models.Entities;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Represents a repository for managing doctors.
    /// </summary>
    public interface ISpecialityRepository
    {
        /// <summary>
        /// Adds a new speciality asynchronously.
        /// </summary>
        /// <param name="speciality">The speciality object to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Speciality speciality);

        /// <summary>
        /// Deletes a speciality asynchronously.
        /// </summary>
        /// <param name="speciality">The speciality object to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Speciality speciality);

        /// <summary>
        /// Retrieves all specialities asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, returning a collection of specialities.</returns>
        Task<IEnumerable<Speciality>> GetAllAsync();

        /// <summary>
        /// Retrieves a speciality by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the speciality.</param>
        /// <returns>A task representing the asynchronous operation, returning the speciality object if found; otherwise, null.</returns>
        Task<Speciality> GetByIdAsync(uint id);

        /// <summary>
        /// Updates an existing speciality asynchronously.
        /// </summary>
        /// <param name="speciality">The speciality object with updated information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Speciality speciality);
    }
}