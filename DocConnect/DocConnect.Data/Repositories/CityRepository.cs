using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public CityRepository(DocConnectContext docConnectContext)
        {
            _docConnectContext = docConnectContext;
        }

        public async Task<IEnumerable<City>> GetAllAsync(int takeAmount, int skipAmount)
        {
            var cities = await _docConnectContext.Cities
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .Skip(skipAmount)
                .Take(takeAmount)
                .ToListAsync();

            return cities;
        }
    }
}
