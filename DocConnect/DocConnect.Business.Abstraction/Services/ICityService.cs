using DocConnect.Business.Models.DTOs.City;

namespace DocConnect.Business.Abstraction.Services
{
    /// <summary>
    /// Interface for a city service that provides operations related to cities.
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Retrieves a collection of cities asynchronously, with optional paging.
        /// </summary>
        /// <param name="takeAmount">The number of cities to retrieve per page.</param>
        /// <param name="page">The page number indicating which set of cities to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation, returning a collection of CityResultDTOs
        /// containing information about the cities.
        /// </returns>
        Task<IEnumerable<CityResultDTO>> GetAllAsync(int takeAmount, int page);
    }
}
