using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public PatientRepository(DocConnectContext docConnectContext)
        {
            _docConnectContext = docConnectContext;
        }

        public async Task<uint> GetPatientIdByUserIdAsync(uint userId)
        {
            var patient = await _docConnectContext.Patients
                .Where(p => p.UserId ==  userId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return patient.Id;
        }
    }
}
