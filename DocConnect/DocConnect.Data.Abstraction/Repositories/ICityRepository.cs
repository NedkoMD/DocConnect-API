using DocConnect.Data.Models.Entities;

namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Represents a repository for managing cities.
    /// </summary>
    public interface ICityRepository
    {
        /// <summary>
        /// Gets all cities from the database.
        /// </summary>
        /// <returns>A list of city entities.</returns>
        Task<IEnumerable<City>> GetAllAsync(int takeAmount, int skipAmount);
    }
}
