using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Models;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Interface for retrieving location data asynchronously.
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// Retrieve a collection of locations with optional paging asynchronously.
        /// </summary>
        /// <param name="takeAmount">The number of locations to retrieve per page.</param>
        /// <param name="skipAmount">The number of locations to skip.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of locations.</returns>
        Task<IEnumerable<Location>> GetAllAsync(int takeAmount, int skipAmount);

        /// <summary>
        /// Retrieve a collection of detailed location models with optional paging asynchronously.
        /// </summary>
        /// <param name="takeAmount">The number of detailed locations to retrieve per page.</param>
        /// <param name="skipAmount">The number of detailed locations to skip.</param>
        /// <returns>A task representing the asynchronous operation, returning a collection of detailed location models.</returns>
        Task<IEnumerable<LocationDetailedModel>> GetAllDetailedLocationsAsync(int takeAmount, int skipAmount);
    }
}