using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Doctor;
using DocConnect.Business.Models.Options;
using DocConnect.Business.Models.Results;
using DocConnect.Business.Models.Utilities;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ImageOptions _imageOptions;

        public DoctorService(
            IMapper mapper,
            IResultFactory resultFactory,
            IDoctorRepository doctorRepository,
            ImageOptions imageOptions)
        {
            _mapper = mapper;
            _resultFactory = resultFactory;
            _doctorRepository = doctorRepository;
            _imageOptions = imageOptions;
        }

        public async Task<IEnumerable<DoctorResultDTO>> GetAllAsync(int takeAmount, int page)
        {
            var skipAmount = page > default(int) ? takeAmount * (page - 1) : default;

            var doctors = await _doctorRepository.GetAllAsync(takeAmount, skipAmount);
            var doctorResultDTOs = _mapper.Map<IEnumerable<DoctorResultDTO>>(doctors);
            doctorResultDTOs = doctorResultDTOs.Select(s => { s.PictureLocation = _imageOptions.DomainName + s.PictureLocation; return s; });

            return doctorResultDTOs;
        }

        public async Task<IEnumerable<DoctorSearchResultDTO>> GetAllDoctorSearchModelsAsync(string name, string specialityName, string locationName, int amount, int page)
        {
            var takeAmount = amount > default(int) ? amount : default;
            var skipAmount = page > default(int) ? takeAmount * (page - 1) : default;

            var doctorSearchModels = await _doctorRepository.GetAllDoctorSearchModelsAsync(name, specialityName, locationName, takeAmount, skipAmount);
            var doctorSearchResultDTOs = _mapper.Map<IEnumerable<DoctorSearchResultDTO>>(doctorSearchModels);
            doctorSearchResultDTOs = doctorSearchResultDTOs.Select(s => { s.ImageUrl = _imageOptions.DomainName + s.ImageUrl; return s; });

            return doctorSearchResultDTOs;
        }

        public async Task<IResult<DoctorResultDTO>> GetByIdAsync(uint id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<DoctorResultDTO>(DoctorMessages.DoctorNotFoundMessage);

                return notFoundResult;
            }

            var doctorResultDTO = _mapper.Map<DoctorResultDTO>(doctor);
            var okResult = _resultFactory.GetOkResult(doctorResultDTO);

            return okResult;
        }

        public async Task<IResult<DoctorResultDTO>> AddAsync(DoctorAddDTO doctorAddDTO)
        {
            var doctor = _mapper.Map<Doctor>(doctorAddDTO);
            await _doctorRepository.AddAsync(doctor);

            var doctorResultDTO = _mapper.Map<DoctorResultDTO>(doctor);
            var okResult = _resultFactory.GetOkResult(doctorResultDTO);

            return okResult;
        }

        public async Task<IResult<DoctorResultDTO>> UpdateAsync(uint id, DoctorUpdateDTO doctorUpdateDTO)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<DoctorResultDTO>(DoctorMessages.DoctorNotFoundMessage);

                return notFoundResult;
            }

            _mapper.Map(doctorUpdateDTO, doctor);
            await _doctorRepository.UpdateAsync(doctor);

            var doctorResultDTO = _mapper.Map<DoctorResultDTO>(doctor);
            var okResult = _resultFactory.GetOkResult(doctorResultDTO);

            return okResult;
        }

        public async Task<IResult<DoctorResultDTO>> DeleteAsync(uint id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<DoctorResultDTO>(DoctorMessages.DoctorNotFoundMessage);

                return notFoundResult;
            }

            await _doctorRepository.DeleteAsync(doctor);

            var noContentResult = _resultFactory.GetNoContentResult<DoctorResultDTO>();

            return noContentResult;
        }

        public async Task<IResult<DetailedDoctorInfoResultDTO>> GetDetailedDoctorInfoByIdAsync(uint id)
        {
            var detailedDoctorInfo = await _doctorRepository.GetDetailedDoctorInfoByIdAsync(id);

            if (detailedDoctorInfo == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<DetailedDoctorInfoResultDTO>(DoctorMessages.DoctorNotFoundMessage);

                return notFoundResult;
            }

            var doctorResultDTO = _mapper.Map<DetailedDoctorInfoResultDTO>(detailedDoctorInfo);
            doctorResultDTO.ImageUrl = _imageOptions.DomainName + detailedDoctorInfo.ImageUrl;
            var okResult = _resultFactory.GetOkResult(doctorResultDTO);

            return okResult;
        }

        public async Task<uint> GetDoctorIdBySpecialistId(uint specialistId)
        {
            var doctorId = await _doctorRepository.GetDoctorIdBySpecialistId(specialistId);

            return doctorId;
        }
    }
}
