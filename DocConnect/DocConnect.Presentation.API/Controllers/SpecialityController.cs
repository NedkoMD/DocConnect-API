using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Models.DTOs.Speciality;
using DocConnect.Presentation.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly ISpecialityService _specialityService;

        public SpecialityController(ISpecialityService specialityService)
        {
            _specialityService = specialityService;
        }

        // GET: api/Speciality
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _specialityService.GetAllAsync();

            return Ok(response);
        }

        // GET: api/Speciality/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] uint id)
        {
            var response = await _specialityService.GetByIdAsync(id);

            return this.HandleResponse(response);
        }

        // POST: api/Speciality
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] SpecialityAddDTO request)
        {
            var response = await _specialityService.AddAsync(request);

            return this.HandleResponse(response);
        }

        // PUT: api/Speciality/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] uint id, [FromBody] SpecialityUpdateDTO request)
        {
            var response = await _specialityService.UpdateAsync(id, request);

            return this.HandleResponse(response);
        }

        // DELETE: api/Speciality/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] uint id)
        {
            var response = await _specialityService.DeleteAsync(id);

            return this.HandleResponse(response);
        }
    }
}
