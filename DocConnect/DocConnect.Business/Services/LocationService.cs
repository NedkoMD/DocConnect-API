using AutoMapper;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Location;
using DocConnect.Data.Abstraction.Repositories;

namespace DocConnect.Business.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LocationResultDTO>> GetAllAsync(int takeAmount, int page)
        {
            var skipAmount = page > default(int) ? takeAmount * (page - 1) : default;

            var locations = await _locationRepository.GetAllAsync(takeAmount, skipAmount);
            var locationResultDTOs = _mapper.Map<IEnumerable<LocationResultDTO>>(locations);

            return locationResultDTOs;
        }

        public async Task<IEnumerable<LocationDetailedResultDTO>> GetAllDetailedLocationsAsync(int takeAmount, int page)
        {
            var skipAmount = page > default(int) ? takeAmount * (page - 1) : default;

            var locations = await _locationRepository.GetAllDetailedLocationsAsync(takeAmount, skipAmount);
            var locationResultDTOs = _mapper.Map<IEnumerable<LocationDetailedResultDTO>>(locations);

            return locationResultDTOs;
        }
    }
}
