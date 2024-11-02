using uTraverse.PlacesAPI.Models;

namespace uTraverse.PlacesAPI.Services
{
    /// <summary>
    /// Handles retrieving place details from the DB
    /// </summary>
    public interface IPlacesService
    {
        /// <summary>
        /// Get details for places with given IDs
        /// </summary>
        /// <param name="ids">A collection of </param>
        /// <returns></returns>
        Task<IEnumerable<Place>> GetPlacesByIdsAsync(IEnumerable<string> ids);
        // The usage of IEnumerable as both return type and parameter type here is justified by benchmarks, showing 5% performance decrease (50us @ 1000 entities) which shall be considered negligible
    }
}