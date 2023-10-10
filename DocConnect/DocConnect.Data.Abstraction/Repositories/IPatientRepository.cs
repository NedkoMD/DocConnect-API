namespace DocConnect.Data.Abstraction.Repositories
{
    /// <summary>
    /// Interface for retrieving patient-related data asynchronously.
    /// </summary>
    public interface IPatientRepository
    {
        /// <summary>
        /// Get the patient's unique identifier by their user ID asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, returning the patient's ID.</returns>
        Task<uint> GetPatientIdByUserIdAsync(uint userId);
    }
}
