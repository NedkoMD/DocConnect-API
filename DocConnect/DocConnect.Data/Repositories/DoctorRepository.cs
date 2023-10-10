using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public DoctorRepository(DocConnectContext docConnectDbContext)
        {
            _docConnectContext = docConnectDbContext;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(int takeAmount, int skipAmount)
        {
            var doctors = await _docConnectContext.Doctors
                .Where(d => !d.IsDeleted)
                .AsNoTracking()
                .Skip(skipAmount)
                .Take(takeAmount)
                .ToListAsync();

            return doctors;
        }

        public async Task<IEnumerable<DoctorSearchModel>> GetAllDoctorSearchModelsAsync(
            string name,
            string specialityName,
            string locationName,
            int takeAmount,
            int skipAmount)
        {
            var specialists = await _docConnectContext.Specialists
                .Include(s => s.Speciality)
                .Include(s => s.Doctor.User)
                .Include(s => s.Doctor.Location.City.State.Country)
                .Where(s => !s.IsDeleted)
                .Select(s => new 
                {
                    Specialist = s,
                    LocationName = s.Doctor.Location.City.Name + ", " + s.Doctor.Location.State.Ansi + ", " + s.Doctor.Location.State.Country.Alpha3
                })
                .Where(s =>
                (specialityName.Equals("") || s.Specialist.Speciality.Name.Equals(specialityName)) &&
                (locationName.Equals("") || s.LocationName.Equals(locationName)) &&
                s.Specialist.Doctor.User != null &&
                (s.Specialist.Doctor.User.FirstName + " " + s.Specialist.Doctor.User.LastName).Contains(name))
                .Skip(skipAmount)
                .Take(takeAmount)
                .Select(s => new DoctorSearchModel
                {
                    Id = s.Specialist.Id,
                    ImageUrl = s.Specialist.Doctor.PictureLocation,
                    FullName = s.Specialist.Doctor.User.FirstName + " " + s.Specialist.Doctor.User.LastName,
                    SpecialityName = s.Specialist.Speciality.Name,
                    Location = s.Specialist.Doctor.Location.Address + ", " + s.Specialist.Doctor.Location.City.Name + ", " + s.Specialist.Doctor.Location.State.Name + " " + s.Specialist.Doctor.Location.Zip + ", " + s.Specialist.Doctor.Location.State.Country.Alpha3
                })
                .AsNoTracking()
                .ToListAsync();


            return specialists;
        }

        public async Task<Doctor> GetByIdAsync(uint id)
        {
            var doctor = await _docConnectContext.Doctors
                .Where(d => !d.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            return doctor;
        }

        public async Task<DetailedDoctorInfo> GetDetailedDoctorInfoByIdAsync(uint id)
        {
            var doctorInfo = await _docConnectContext.Specialists
                .Include(s => s.Doctor.Location.City.State.Country)
                .Include(s => s.Speciality)
                .Include(s => s.Doctor.User)
                .Where(s => !s.IsDeleted && s.Doctor.User != null)
                .AsNoTracking()
                .Select(s => new DetailedDoctorInfo()
                {
                    FullName = s.Doctor.User.FirstName + " " + s.Doctor.User.LastName,
                    SpecialityName = s.Speciality.Name,
                    ImageUrl = s.Doctor.PictureLocation,
                    Address = s.Doctor.Location.Address + ", " + s.Doctor.Location.City.Name + ", " + s.Doctor.Location.State.Name + " " + s.Doctor.Location.Zip + ", " + s.Doctor.Location.State.Country.Alpha3,
                    Summary = s.Doctor.Summary,
                    Id = s.Id
                })
                .FirstOrDefaultAsync(s => s.Id == id);

            return doctorInfo;
        }

        public async Task AddAsync(Doctor doctor)
        {
            _docConnectContext.Doctors.Add(doctor);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _docConnectContext.Doctors.Update(doctor);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Doctor doctor)
        {
            _docConnectContext.Doctors.Remove(doctor);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task<uint> GetDoctorIdBySpecialistId(uint specialistId)
        {
            var doctor = await _docConnectContext.Doctors
                .Include(s => s.Specialists)
                .Where(s => s.Specialists.FirstOrDefault().Id == specialistId)
                .FirstOrDefaultAsync();

            return doctor.Id;
        }
    }
}
