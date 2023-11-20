using AutoMapper;
using courses_registration.DTO;
using courses_registration.Interfaces;
using courses_registration.Models;
using courses_registration.Repositories;
using courses_registration.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses_registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IMapper _mapper;

        public LookupsController(ILookupRepository lookupRepository, IMapper mapper) 
        {
            _lookupRepository = lookupRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(LookupDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetLookup(int id)
        {
            if (!_lookupRepository.LookupExists(id))
                return NotFound();

            var lookup = _mapper.Map<LookupDTO>(_lookupRepository.GetLookup(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(lookup);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLookup([FromBody] LookupDTO lookup)
        {
            if (lookup == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!_lookupRepository.CreateLookup(_mapper.Map<Lookup>(lookup)))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLookup(int id, [FromBody] Lookup lookup)
        {
            if (lookup == null || id != lookup.Id)
                return BadRequest(ModelState);

            if (!_lookupRepository.LookupExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lookupRepository.UpdateLookup(_mapper.Map<Lookup>(lookup)))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

         
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLookup(int id)
        {

            if (!_lookupRepository.LookupExists(id))
                return NotFound();

            var lookupToDelete = _lookupRepository.GetLookup(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lookupRepository.Delete(lookupToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting lookup");
            }
            return NoContent();
        }

        [HttpGet("getLookupValue")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        public IActionResult GetLookupValue(string lookupName, int lookupId)
        {

            var lookupValue = _lookupRepository.GetLookupValue(lookupName,lookupId);

            if (lookupValue == null)
                return NotFound();

            return Ok(lookupValue);
        }

        [HttpGet("getLookupValues")]
        [ProducesResponseType(200, Type = typeof(LookupDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetLookupValues(string lookupName)
        {

            var lookupValues = _mapper.Map <LookupDTO>(_lookupRepository.GetLookupValues(lookupName));

            if (lookupValues == null)
                return NotFound();

            return Ok(lookupValues);
        }
    }

}
