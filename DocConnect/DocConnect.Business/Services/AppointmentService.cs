using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.Results;
using DocConnect.Business.Models.Utilities;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AppointmentService(
            IMapper mapper,
            IResultFactory resultFactory,
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository)
        {
            _mapper = mapper;
            _resultFactory = resultFactory;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<AppointmentResultDTO>> GetAllAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            var appointmentResultDTOs = _mapper.Map<IEnumerable<AppointmentResultDTO>>(appointments);

            return appointmentResultDTOs;
        }

        public async Task<IEnumerable<AppointmentDetailedResultDTO>> GetAllDetailedAppointmentsAsync(uint userId)
        {
            var appointments = await _appointmentRepository.GetAllDetailedAppointmentAsync(userId);

            var appointmentResultDTOs = _mapper.Map<IEnumerable<AppointmentDetailedResultDTO>>(appointments);

            return appointmentResultDTOs;
        }

        public async Task<IResult<AppointmentResultDTO>> GetByIdAsync(long id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<AppointmentResultDTO>(AppointmentMessages.AppointmentNotFoundMessage);

                return notFoundResult;
            }

            var appointmentResultDTO = _mapper.Map<AppointmentResultDTO>(appointment);
            var okResult = _resultFactory.GetOkResult(appointmentResultDTO);

            return okResult;
        }

        public async Task<IResult<AppointmentResultDTO>> AddAsync(AppointmentAddDTO appointmentAddDTO)
        {
            if(appointmentAddDTO.Hour < 9 || appointmentAddDTO.Hour > 16 || 
                appointmentAddDTO.TimeSlot.DayOfWeek > DayOfWeek.Friday || appointmentAddDTO.TimeSlot.DayOfWeek < DayOfWeek.Monday)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<AppointmentResultDTO>(AppointmentMessages.AppointmentTimeSlotNotInWorkingHours);

                return badRequestResult;
            }

            var appointment = _mapper.Map<Appointment>(appointmentAddDTO);

            if(appointment.TimeSlot.Date <= DateTime.Now.Date || appointment.TimeSlot.Date > DateTime.Now.AddDays(30).Date)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<AppointmentResultDTO>(AppointmentMessages.AppointmentOutOfDateMessage);

                return badRequestResult;
            }

            var existingAppointment = _appointmentRepository.GetAllByPatientIdAsync(appointmentAddDTO.UserId)
            .GetAwaiter()
            .GetResult()
            .FirstOrDefault(s => s.TimeSlot.Date == appointment.TimeSlot.Date && s.TimeSlot.Hour == appointment.TimeSlot.Hour);

            if (existingAppointment != null)
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<AppointmentResultDTO>(AppointmentMessages.PatientAppointmentHourAlreadyTaken);

                return badRequestResult;
            }

            var appointmentsByPatient = _appointmentRepository
                .GetAllByPatientIdAsync(appointmentAddDTO.UserId)
                .GetAwaiter()
                .GetResult()
                .Where(s => s.TimeSlot.Date == appointment.TimeSlot && s.TimeSlot.Hour == appointment.TimeSlot.Hour);

            if (appointmentsByPatient.Any())
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<AppointmentResultDTO>(AppointmentMessages.PatientAppointmentHourAlreadyTaken);

                return badRequestResult;
            }

            var date = DateTime.Now;

            var appointmentsByDoctor = _appointmentRepository
                .GetAllByDoctorIdAsync(appointmentAddDTO.DoctorId, date, date.AddDays(30))
                .GetAwaiter()
                .GetResult()
                .Where(s => s.TimeSlot.Date == appointment.TimeSlot.Date && s.TimeSlot.Hour == appointment.TimeSlot.Hour);

            if (appointmentsByDoctor.Any())
            {
                var badRequestResult = _resultFactory.GetBadRequestResult<AppointmentResultDTO>(AppointmentMessages.DoctorAppointmentHourAlreadyTaken);

                return badRequestResult;
            }

            var patientId = await _patientRepository.GetPatientIdByUserIdAsync(appointmentAddDTO.UserId);
            var doctorId = await _doctorRepository.GetDoctorIdBySpecialistId(appointmentAddDTO.DoctorId);

            appointment.PatientId = patientId;
            appointment.DoctorId = doctorId;
            await _appointmentRepository.AddAsync(appointment);

            var appointmentResultDTO = _mapper.Map<AppointmentResultDTO>(appointment);
            var okResult = _resultFactory.GetOkResult(appointmentResultDTO);

            return okResult;
        }

        public async Task<IResult<AppointmentResultDTO>> UpdateAsync(long id, AppointmentUpdateDTO appointmentUpdateDTO)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<AppointmentResultDTO>(AppointmentMessages.AppointmentNotFoundMessage);

                return notFoundResult;
            }

            _mapper.Map(appointmentUpdateDTO, appointment);
            await _appointmentRepository.UpdateAsync(appointment);

            var appointmentResultDTO = _mapper.Map<AppointmentResultDTO>(appointment);
            var okResult = _resultFactory.GetOkResult(appointmentResultDTO);

            return okResult;
        }

        public async Task<IResult<AppointmentDetailedEmailDTO>> DeleteAsync(long id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<AppointmentDetailedEmailDTO>(AppointmentMessages.AppointmentNotFoundMessage);

                return notFoundResult;
            }

            var appointmentDetailedModel = await _appointmentRepository.GetDetailedAppointmentByIdAsync(id);
            var appointmentDetailedResultDTO = _mapper.Map<AppointmentDetailedEmailDTO>(appointmentDetailedModel);

            await _appointmentRepository.DeleteAsync(appointment);

            var okResult = _resultFactory.GetOkResult(appointmentDetailedResultDTO);

            return okResult;
        }

        public async Task<AppointmentDoctorResultDTO> GetAllByDoctorIdAsync(uint doctorId)
        {
            var date = DateTime.UtcNow;

            var appointments = await _appointmentRepository.GetAllByDoctorIdAsync(doctorId, date, date.AddDays(30));
            var appointmentResultDTOs = _mapper.Map<IEnumerable<AppointmentResultDTO>>(appointments);
            var appointmentDoctorResultDTO = _mapper.Map<AppointmentDoctorResultDTO>(appointmentResultDTOs);
            appointmentDoctorResultDTO.DoctorId = doctorId;
            appointmentDoctorResultDTO.Appointments = appointmentResultDTOs;

            return appointmentDoctorResultDTO;
        }

        public async Task<IEnumerable<AppointmentResultDTO>> GetAllByPatientIdAsync(uint userId)
        {
            var appointment = await _appointmentRepository.GetAllByPatientIdAsync(userId);
            var appointmentResultDTO = _mapper.Map<IEnumerable<AppointmentResultDTO>>(appointment);

            return appointmentResultDTO;
        }

        public async Task<IEnumerable<AppointmentDetailedResultDTO>> GetAllFutureAppointmentsByUserIdAsync(uint userId)
        {
            var appointment = await _appointmentRepository.GetAllFutureAppoimtmentsByUserIdAsync(userId);
            var appointmentResultDTO = _mapper.Map<IEnumerable<AppointmentDetailedResultDTO>>(appointment);

            return appointmentResultDTO;
        }

        public async Task<IEnumerable<AppointmentDetailedResultDTO>> GetAllPastAppointmentsByUserIdAsync(uint userId)
        {
            var appointment = await _appointmentRepository.GetAllPastAppointmentsByUserIdAsync(userId);
            var appointmentResultDTO = _mapper.Map<IEnumerable<AppointmentDetailedResultDTO>>(appointment);

            return appointmentResultDTO;
        }
    }
}
