using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocConnect.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DocConnectContext _docConnectContext;

        public AppointmentRepository(DocConnectContext docConnectDbContext)
        {
            _docConnectContext = docConnectDbContext;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            var appointments = await _docConnectContext.Appointments
                .Where(a => !a.IsDeleted)
                .AsNoTracking()
                .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<AppointmentDetailedModel>> GetAllDetailedAppointmentAsync(uint userId)
        {
            var appointmentDetailedModels = await _docConnectContext.Appointments
                .Include(a => a.Patient.User)
                .Include(a => a.Doctor.User)
                .Include(a => a.Doctor.Specialists)
                .Include(s => s.Doctor.Location.City.State.Country)
                .Where(a => !a.IsDeleted && a.Patient.UserId == userId && a.Doctor.User != null)
                .Select(a => new AppointmentDetailedModel()
                {
                    Id = a.Id,
                    PatientFullName = a.Patient.User.FirstName + " " + a.Patient.User.LastName,
                    DoctorFullName = a.Doctor.User.FirstName + " " + a.Doctor.User.LastName,
                    TimeSlot = a.TimeSlot,
                    Location = a.Doctor.Location.Address + ", " + a.Doctor.Location.City.Name + ", " + a.Doctor.Location.State.Name + " " + a.Doctor.Location.Zip + ", " + a.Doctor.Location.State.Country.Alpha3,
                    Patient = a.Patient,
                    Doctor = a.Doctor,
                    SpecialityName = a.Doctor.Specialists.FirstOrDefault().Speciality.Name
                })
                .OrderBy(s => s.TimeSlot)
                .AsNoTracking()
                .ToListAsync();

            return appointmentDetailedModels;
        }

        public async Task<AppointmentDetailedModel> GetDetailedAppointmentByIdAsync(long Id)
        {
            var appointmentDetailedModels = await _docConnectContext.Appointments
                .Include(a => a.Patient.User)
                .Include(a => a.Doctor.User)
                .Include(s => s.Doctor.Location.City.State.Country)
                .Include(a => a.Doctor.Specialists)
                .Where(a => !a.IsDeleted && a.Id == Id && a.Doctor.User != null)
                .Select(a => new AppointmentDetailedModel()
                {
                    Id = a.Id,
                    PatientFullName = a.Patient.User.FirstName + " " + a.Patient.User.LastName,
                    DoctorFullName = a.Doctor.User.FirstName + " " + a.Doctor.User.LastName,
                    TimeSlot = a.TimeSlot,
                    Location = a.Doctor.Location.Address + ", " + a.Doctor.Location.City.Name + ", " + a.Doctor.Location.State.Name + " " + a.Doctor.Location.Zip + ", " + a.Doctor.Location.State.Country.Alpha3,
                    Patient = a.Patient,
                    Doctor = a.Doctor,
                    SpecialityName = a.Doctor.Specialists.FirstOrDefault().Speciality.Name
                })
                .OrderBy(s => s.TimeSlot)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return appointmentDetailedModels;
        }

        public async Task<Appointment> GetByIdAsync(long id)
        {
            var appointment = await _docConnectContext.Appointments
                .Where(a => !a.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return appointment;
        }

        public async Task AddAsync(Appointment appointment)
        {
            _docConnectContext.Appointments.Add(appointment);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _docConnectContext.Appointments.Update(appointment);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _docConnectContext.Appointments.Remove(appointment);
            await _docConnectContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllByDoctorIdAsync(uint doctorId, DateTime start, DateTime end)
        {
            var appointments = await _docConnectContext.Appointments
                .Where(a => !a.IsDeleted && a.Doctor.Specialists.FirstOrDefault().Id == doctorId && a.TimeSlot > start && a.TimeSlot <= end)
                .AsNoTracking()
                .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAllByPatientIdAsync(uint userId)
        {
            var appointments = await _docConnectContext.Appointments
                .Include(a => a.Patient)
                .Where(a => !a.IsDeleted && a.Patient.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            return appointments;
        }

        public async Task<Appointment> GetByDoctorIdAndPatientIdAsync(uint doctorId, uint userId)
        {
            var appointments = await _docConnectContext.Appointments
                .Include(a => a.Patient)
                .Where(a => !a.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Patient.UserId == userId && a.DoctorId == doctorId);

            return appointments;
        }

        public async Task<IEnumerable<AppointmentDetailedModel>> GetAllFutureAppoimtmentsByUserIdAsync(uint userId)
        {
            var appointments = await _docConnectContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialists)
                .Where(a => !a.IsDeleted && a.Patient.UserId == userId && a.TimeSlot > DateTime.Now)
                .Select(a => new AppointmentDetailedModel()
                {
                    Id = a.Id,
                    PatientFullName = a.Patient.User.FirstName + " " + a.Patient.User.LastName,
                    DoctorFullName = a.Doctor.User.FirstName + " " + a.Doctor.User.LastName,
                    TimeSlot = a.TimeSlot,
                    Location = a.Doctor.Location.Address + ", " + a.Doctor.Location.City.Name + ", " + a.Doctor.Location.State.Name + " " + a.Doctor.Location.Zip + ", " + a.Doctor.Location.State.Country.Alpha3,
                    Patient = a.Patient,
                    Doctor = a.Doctor,
                    SpecialityName = a.Doctor.Specialists.FirstOrDefault().Speciality.Name
                })
                .OrderBy(s => s.TimeSlot)
                .AsNoTracking()
                .ToListAsync();

            return appointments;
        }

        public async Task<IEnumerable<AppointmentDetailedModel>> GetAllPastAppointmentsByUserIdAsync(uint userId)
        {
            var appointments = await _docConnectContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialists)
                .Where(a => !a.IsDeleted && a.Patient.UserId == userId && a.TimeSlot < DateTime.Now)
                .Select(a => new AppointmentDetailedModel()
                {
                    Id = a.Id,
                    PatientFullName = a.Patient.User.FirstName + " " + a.Patient.User.LastName,
                    DoctorFullName = a.Doctor.User.FirstName + " " + a.Doctor.User.LastName,
                    TimeSlot = a.TimeSlot,
                    Location = a.Doctor.Location.Address + ", " + a.Doctor.Location.City.Name + ", " + a.Doctor.Location.State.Name + " " + a.Doctor.Location.Zip + ", " + a.Doctor.Location.State.Country.Alpha3,
                    Patient = a.Patient,
                    Doctor = a.Doctor,
                    SpecialityName = a.Doctor.Specialists.FirstOrDefault().Speciality.Name
                })
                .OrderByDescending(s => s.TimeSlot)
                .AsNoTracking()
                .ToListAsync();

            return appointments;
        }
    }
}
