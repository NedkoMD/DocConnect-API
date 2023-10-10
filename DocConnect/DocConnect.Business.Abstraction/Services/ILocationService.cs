using DocConnect.Business.Models.DTOs.Location;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Interface for retrieving location information asynchronously.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Retrieve a collection of locations with optional paging.
        /// </summary>
        Task<IEnumerable<LocationResultDTO>> GetAllAsync(int takeAmount, int page);

        /// <summary>
        /// Retrieve a collection of detailed location information with optional paging.
        /// </summary>
        Task<IEnumerable<LocationDetailedResultDTO>> GetAllDetailedLocationsAsync(int takeAmount, int page);
    }
}