using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Flight.Business; // Make sure to add the appropriate using statements
using Flight.Models;
using Flight.DTOs;

namespace Flight.API.Controllers
{
    [Route("api/aeroplanes")]
    [ApiController]
    public class AeroplaneController : ControllerBase
    {
        private readonly AeroplaneService _aeroplaneService;

        public AeroplaneController(AeroplaneService aeroplaneService)
        {
            _aeroplaneService = aeroplaneService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aeroplane>> GetByIdAsync(int id)
        {
            var aeroplane = await _aeroplaneService.GetByIdAsync(id);
            if (aeroplane == null)
            {
                return NotFound();
            }

            return Ok(aeroplane);
        }

        [HttpGet]
        public async Task<ActionResult<PublishAeroplane>> GetAeroplanes()
        {
            var aeroplanes = await _aeroplaneService.GetAeroplanes();
            return Ok(aeroplanes);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IReadOnlyList<Aeroplane>>> GetAeroplanesByFilterAsync([FromBody] Expression<Func<Aeroplane, bool>> filter)
        {
            var aeroplanes = await _aeroplaneService.GetAeroplanesByFilterAsync(filter);
            return Ok(aeroplanes);
        }

        [HttpPost]
        public async Task<ActionResult<Aeroplane>> AddAeroplaneAsync([FromBody] Aeroplane aeroplane)
        {
            var addedAeroplane = await _aeroplaneService.AddAeroplaneAsync(aeroplane);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = addedAeroplane.Id }, addedAeroplane);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAeroplaneAsync(int id, [FromBody] Aeroplane aeroplane)
        {
            if (id != aeroplane.Id)
            {
                return BadRequest();
            }

            await _aeroplaneService.UpdateAeroplaneAsync(aeroplane);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAeroplaneAsync(int id)
        {
            await _aeroplaneService.DeleteAeroplaneAsync(id);
            return NoContent();
        }

        [HttpPost("publish")]
        public IActionResult PublishAeroplane()
        {
            if (_aeroplaneService.PublishAeroplane())
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to publish aeroplanes.");
            }
        }
    }
}
