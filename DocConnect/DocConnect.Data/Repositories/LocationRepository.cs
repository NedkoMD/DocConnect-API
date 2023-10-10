using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public LocationRepository(DocConnectContext docConnectDbContext)
        {
            _docConnectContext = docConnectDbContext;
        }

        public async Task<IEnumerable<Location>> GetAllAsync(int takeAmount, int skipAmount)
        {
            var locations = await _docConnectContext.Locations
                .Where(l => !l.IsDeleted)
                .AsNoTracking()
                .Skip(skipAmount)
                .Take(takeAmount)
                .ToListAsync();

            return locations;
        }

        public async Task<IEnumerable<LocationDetailedModel>> GetAllDetailedLocationsAsync(int takeAmount, int skipAmount)
        {
            var locations = await _docConnectContext.Locations
                .Include(l => l.City.State.Country)
                .Where(l => !l.IsDeleted)
                .AsNoTracking()
                .Skip(skipAmount)
                .Take(takeAmount)
                .Select(l => new LocationDetailedModel
                {
                    Name = l.City.Name + ", " + l.City.State.Ansi + ", " + l.City.State.Country.Alpha3
                })
                .Distinct()
                .OrderBy(l => l.Name)
                .ToListAsync();

            return locations;
        }
    }
}
