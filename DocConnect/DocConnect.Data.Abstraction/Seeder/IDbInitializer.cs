namespace DocConnect.Data.Abstraction.Seeder
{
    /// <summary>
    /// Represents an interface for initializing the database.
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// Initializes the database asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task InitializeAsync();
    }
}