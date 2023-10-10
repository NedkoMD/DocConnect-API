using DocConnect.Business.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: api/Speciality
        [HttpGet("{takeAmount}/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromRoute] int takeAmount, [FromRoute] int page)
        {
            var response = await _cityService.GetAllAsync(takeAmount, page);

            return Ok(response);
        }
    }
}
