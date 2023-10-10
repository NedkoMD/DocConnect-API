using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public SpecialityRepository(DocConnectContext docConnectDbContext)
        {
            _docConnectContext = docConnectDbContext;
        }

        public async Task<IEnumerable<Speciality>> GetAllAsync()
        {
            var specialities = await _docConnectContext.Specialities
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.Name)
                .AsNoTracking()
                .ToListAsync();

            return specialities;
        }

        public async Task<Speciality> GetByIdAsync(uint id)
        {
            var specialities = await _docConnectContext.Specialities
                .Where(s => !s.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            return specialities;
        }

        public async Task AddAsync(Speciality speciality)
        {
            _docConnectContext.Specialities.Add(speciality);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Speciality speciality)
        {
            _docConnectContext.Specialities.Update(speciality);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Speciality speciality)
        {
            _docConnectContext.Specialities.Remove(speciality);
            await _docConnectContext.SaveChangesAsync();
        }
    }
}
