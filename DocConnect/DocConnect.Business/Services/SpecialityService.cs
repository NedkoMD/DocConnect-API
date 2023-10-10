using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Speciality;
using DocConnect.Business.Models.Results;
using DocConnect.Business.Models.Utilities;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;

namespace DocConnect.Business.Services
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;
        private readonly ISpecialityRepository _specialityRepository;

        public SpecialityService(
            IMapper mapper,
            IResultFactory resultFactory,
            ISpecialityRepository specialityRepository)
        {
            _mapper = mapper;
            _resultFactory = resultFactory;
            _specialityRepository = specialityRepository;
        }

        public async Task<IEnumerable<SpecialityResultDTO>> GetAllAsync()
        {
            var specialities = await _specialityRepository.GetAllAsync();
            var specialityResultDTOs = _mapper.Map<IEnumerable<SpecialityResultDTO>>(specialities);

            return specialityResultDTOs;
        }

        public async Task<IResult<SpecialityResultDTO>> GetByIdAsync(uint id)
        {
            var speciality = await _specialityRepository.GetByIdAsync(id);

            if (speciality == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<SpecialityResultDTO>(SpecialityMessages.SpecialityNotFoundMessage);

                return notFoundResult;
            }

            var specialityResultDTO = _mapper.Map<SpecialityResultDTO>(speciality);
            var okResult = _resultFactory.GetOkResult(specialityResultDTO);

            return okResult;
        }

        public async Task<IResult<SpecialityResultDTO>> AddAsync(SpecialityAddDTO specialityAddDTO)
        {
            var speciality = _mapper.Map<Speciality>(specialityAddDTO);
            await _specialityRepository.AddAsync(speciality);

            var specialityResultDTO = _mapper.Map<SpecialityResultDTO>(speciality);
            var okResult = _resultFactory.GetOkResult(specialityResultDTO);

            return okResult;
        }

        public async Task<IResult<SpecialityResultDTO>> UpdateAsync(uint id, SpecialityUpdateDTO specialityUpdateDTO)
        {
            var speciality = await _specialityRepository.GetByIdAsync(id);

            if (speciality == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<SpecialityResultDTO>(SpecialityMessages.SpecialityNotFoundMessage);

                return notFoundResult;
            }

            _mapper.Map(specialityUpdateDTO, speciality);
            await _specialityRepository.UpdateAsync(speciality);

            var specialityResultDTO = _mapper.Map<SpecialityResultDTO>(speciality);
            var okResult = _resultFactory.GetOkResult(specialityResultDTO);

            return okResult;
        }

        public async Task<IResult<SpecialityResultDTO>> DeleteAsync(uint id)
        {
            var speciality = await _specialityRepository.GetByIdAsync(id);

            if (speciality == null)
            {
                var notFoundResult = _resultFactory.GetNotFoundResult<SpecialityResultDTO>(SpecialityMessages.SpecialityNotFoundMessage);

                return notFoundResult;
            }

            await _specialityRepository.DeleteAsync(speciality);

            var noContentResult = _resultFactory.GetNoContentResult<SpecialityResultDTO>();

            return noContentResult;
        }
    }
}
