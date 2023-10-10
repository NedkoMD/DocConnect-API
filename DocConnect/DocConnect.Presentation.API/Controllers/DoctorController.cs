using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Doctor;
using DocConnect.Presentation.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // GET: api/Doctor/5/5
        [HttpGet("{takeAmount}/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromRoute] int takeAmount, [FromRoute] int page)
        {
            var response = await _doctorService.GetAllAsync(takeAmount, page);

            return Ok(response);
        }

        // GET: api/Doctor/Peter/Dermatology/Ada/5/5
        [HttpGet("search-models/{amount}/{page}")]
        public async Task<IEnumerable<DoctorSearchResultDTO>> GetAllSearchModelsAsync(
            [FromRoute] int amount,
            [FromRoute] int page,
            string name = "",
            string specialityName = "",
            string locationName = "")
        {
            var specialists = await _doctorService.GetAllDoctorSearchModelsAsync(name, specialityName, locationName, amount, page);

            return specialists;
        }

        // GET: api/Doctor/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] uint id)
        {
            var response = await _doctorService.GetByIdAsync(id);

            return this.HandleResponse(response);
        }

        // GET: api/Doctor/5
        [HttpGet("detailed-info/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDetailedDoctorInfoByIdAsync([FromRoute] uint id)
        {
            var response = await _doctorService.GetDetailedDoctorInfoByIdAsync(id);

            return this.HandleResponse(response);
        }

        // POST: api/Doctor
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] DoctorAddDTO request)
        {
            var response = await _doctorService.AddAsync(request);

            return this.HandleResponse(response);
        }

        // PUT: api/Doctor/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] uint id, [FromBody] DoctorUpdateDTO request)
        {
            var response = await _doctorService.UpdateAsync(id, request);

            return this.HandleResponse(response);
        }

        // DELETE: api/Doctor/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] uint id)
        {
            var response = await _doctorService.DeleteAsync(id);

            return this.HandleResponse(response);
        }
    }
}
