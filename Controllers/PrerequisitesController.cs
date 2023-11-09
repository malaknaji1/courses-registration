using AutoMapper;
using courses_registration.DTO;
using courses_registration.Interfaces;
using courses_registration.Models;
using courses_registration.Repositories;
using courses_registration.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace courses_registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrerequisitesController : ControllerBase
    {
        private readonly IPrerequisiteRepository _prerequisiteRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<PrerequisiteDTO> _prerequisiteValidator;

        public PrerequisitesController(IPrerequisiteRepository prerequisiteRepository, IMapper mapper, IValidator<PrerequisiteDTO> prerequisiteValidator)
        {
            _prerequisiteRepository = prerequisiteRepository;
            _mapper = mapper;
            _prerequisiteValidator = prerequisiteValidator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PrerequisiteDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getPrerequisite(int id)
        {
            var prerequisite = _prerequisiteRepository.GetPrerequisite(id);

            if (prerequisite == null)
                return NotFound();

            var prerequisiteMap = _mapper.Map<PrerequisiteDTO>(prerequisite);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(prerequisiteMap);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePrerequisite([FromBody] PrerequisiteDTO prerequisite)
        {
            if (prerequisite == null)
                return BadRequest(ModelState);

            if (prerequisite.CourseId == prerequisite.PrerequisiteId)
            {
                ModelState.AddModelError("", "Course and Prerequisite course cannot be the same");
                return StatusCode(422, ModelState);
            }
            if (_prerequisiteRepository.IsPrerequisiteCourseExisits(prerequisite.CourseId,prerequisite.PrerequisiteId))
            {
                ModelState.AddModelError("", "Prerequisite for this course already exists");
                return StatusCode(422, ModelState);
            }

            var validationResult = _prerequisiteValidator.Validate(prerequisite);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_prerequisiteRepository.CreatePrerequisite(_mapper.Map<Prerequisite>(prerequisite)))
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
        public IActionResult UpdatePrerequisite(int id, [FromBody] PrerequisiteDTO prerequisite)
        {
            if (prerequisite == null || id != prerequisite.Id)
                return BadRequest(ModelState);

            if (prerequisite.CourseId == prerequisite.PrerequisiteId)
            {
                ModelState.AddModelError("", "Course and Prerequisite course cannot be the same");
                return StatusCode(422, ModelState);
            }

            if (!_prerequisiteRepository.PrerequisiteExists(id))
                return NotFound();

            if (_prerequisiteRepository.IsPrerequisiteCourseExisits(prerequisite.CourseId, prerequisite.PrerequisiteId))
            {
                ModelState.AddModelError("", "Prerequisite for this course already exists");
                return StatusCode(422, ModelState);
            }

            var validationResult = _prerequisiteValidator.Validate(prerequisite);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_prerequisiteRepository.UpdatePrerequisite(_mapper.Map<Prerequisite>(prerequisite)))
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
        public IActionResult SoftDeletePrerequisite(int id)
        {
            var prerequisiteToDelete = _prerequisiteRepository.GetPrerequisite(id);
            
            if (prerequisiteToDelete == null )
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_prerequisiteRepository.SoftDeletePrerequisite(prerequisiteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting prerequisite");
            }
            return NoContent();
        }
    }
}
