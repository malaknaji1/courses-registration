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
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<EnrollmentDTO> _enrollmentValidator;

        public EnrollmentsController(IEnrollmentRepository enrollmentRepository , IMapper mapper, IValidator<EnrollmentDTO> enrollmentValidator) {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
            _enrollmentValidator = enrollmentValidator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EnrollmentDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getEnrollment(int id)
        {
            if (!_enrollmentRepository.EnrollmentExists(id))
                return NotFound();

            var enrollment = _mapper.Map<EnrollmentDTO>(_enrollmentRepository.GetEnrollment(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(enrollment);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEnrollment([FromBody] EnrollmentDTO enrollment)
        {
            if (enrollment == null)
                return BadRequest(ModelState);

            if (_enrollmentRepository.IsStudentRegistered(enrollment.StudentId,enrollment.CourseId))
            {
                ModelState.AddModelError("", "Student already registered in this course");
                return StatusCode(422, ModelState);
            }

            if (!_enrollmentRepository.IsCompletePrerequisites(enrollment.StudentId, enrollment.CourseId))
            {
                ModelState.AddModelError("", "Student has not completed all prerequisites for the course");
                return StatusCode(422, ModelState);
            }

            var validationResult = _enrollmentValidator.Validate(enrollment);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_enrollmentRepository.CreateEnrollment(_mapper.Map<Enrollment>(enrollment)))
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
        public IActionResult UpdateIsComplete(int id, [FromQuery] bool IsComplete)
        {
            if (!_enrollmentRepository.EnrollmentExists(id))
                return NotFound();

            var enrollmentToUpdate = _enrollmentRepository.GetEnrollment(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_enrollmentRepository.ChangeIsComplete(enrollmentToUpdate, IsComplete))
            {
                ModelState.AddModelError("", "Something went wrong updating enrollment");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult SoftDeleteEnrollment(int id)
        {

            if (!_enrollmentRepository.EnrollmentExists(id))
                return NotFound();

            var enrollmentToDelete = _enrollmentRepository.GetEnrollment(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_enrollmentRepository.SoftDeleteEnrollment(enrollmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting enrollment");
            }
            return NoContent();
        }
    }
}
