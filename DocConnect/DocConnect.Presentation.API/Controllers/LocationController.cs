using DocConnect.Business.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{takeAmount}/{page}")]
        public async Task<IActionResult> GetAllAsync([FromRoute] int takeAmount, [FromRoute] int page)
        {
            var locations = await _locationService.GetAllAsync(takeAmount, page);

            return Ok(locations);
        }

        [HttpGet("detailed-location/{takeAmount}/{page}")]
        public async Task<IActionResult> GetAllDetailedLocationAsync([FromRoute] int takeAmount, [FromRoute] int page)
        {
            var detailedLocations = await _locationService.GetAllDetailedLocationsAsync(takeAmount, page);

            return Ok(detailedLocations);
        }
    }
}
