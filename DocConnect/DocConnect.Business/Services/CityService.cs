using AutoMapper;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.City;
using DocConnect.Data.Abstraction.Repositories;

namespace DocConnect.Business.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityResultDTO>> GetAllAsync(int takeAmount, int page)
        {
            var skipAmount = page > default(int) ? takeAmount * (page - 1) : default;

            var cities = await _cityRepository.GetAllAsync(takeAmount, skipAmount);
            var cityResultDTOs = _mapper.Map<IEnumerable<CityResultDTO>>(cities);

            return cityResultDTOs;
        }
    }
}
