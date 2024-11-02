using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using uTraverse.PlacesAPI.Data;
using uTraverse.PlacesAPI.Exceptions;
using uTraverse.PlacesAPI.Models;

namespace uTraverse.PlacesAPI.Services;

/// <summary>
/// Handles retrieving places details from the DB
/// </summary>
/// <param name="logger">Logger for internal usage</param>
/// <param name="db">DbContext for querying</param>
public class PlacesService(ILogger<PlacesService> logger, PlacesDbContext db, IDistributedCache cache) : IPlacesService
{
    public async Task<IEnumerable<Place>> GetPlacesByIdsAsync(IEnumerable<string> ids)
    {
        List<Place> places = [];

        // Convert to HashSet for better performance
        var idsHash = ids.ToHashSet();
        HashSet<string> dbIds = [];

        // Retrieve cached places
        foreach (var id in idsHash)
        {
            string? placeString = await cache.GetStringAsync(id);

            if (placeString is null)
            {
                dbIds.Add(id);
                continue;
            }

            Place place = JsonSerializer.Deserialize<Place>(placeString)!;      

            places.Add(place);
        }

        logger.LogDebug("Retrieving places with IDs: {ids}", string.Join(", ", dbIds));

        Place[] placesDb= [];
        if (dbIds.Count!=0)
            placesDb = await db.Places.Where(e => dbIds.Contains(e.XID)).ToArrayAsync();

        //// Cache places
        foreach (var placeDb in placesDb)
        {
            string? placeString = JsonSerializer.Serialize(placeDb);
            await cache.SetStringAsync(placeDb.XID, placeString, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            });
        }

        // Get all places whose ID are in the list
        places = [.. places, .. placesDb];

        // Throw exception if no places were found
        if (places is null)
        {
            logger.LogWarning("Couldn't find places with given IDs");
            throw new PlaceNotFoundException($"No places with such IDs were found");
        }

        logger.LogDebug("Retrieval succeeded");

        return places;
    }
}
