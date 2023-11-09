using AutoMapper;
using courses_registration.DTO;
using courses_registration.Interfaces;
using courses_registration.Models;
using courses_registration.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace courses_registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CourseDTO> _courseValidator;

        public CoursesController(ICourseRepository courseRepository, IMapper mapper, IValidator<CourseDTO> courseValidator)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _courseValidator = courseValidator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CourseDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult getCourse(int id)
        {
            if (!_courseRepository.CourseExists(id))
                return NotFound();

            var course =_mapper.Map<CourseDTO>( _courseRepository.GetCourse(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromBody] CourseDTO course)
        {
            if (course == null)
                return BadRequest(ModelState);

            if (_courseRepository.IsCourseNameExisits(course.Name))
            {
                ModelState.AddModelError("", "Course already exists");
                return StatusCode(422, ModelState);
            }

            var validationResult = _courseValidator.Validate(course);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_courseRepository.CreateCourse(_mapper.Map<Course>(course)))
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
        public IActionResult UpdateCourse(int id, [FromBody] CourseDTO course)
        {
            if (course == null ||id != course.Id)
                return BadRequest(ModelState);

            if (!_courseRepository.CourseExists(id))
                return NotFound();

            if (_courseRepository.IsCourseNameExisits(course.Name))
            {
                ModelState.AddModelError("", "Course already exists");
                return StatusCode(422, ModelState);
            }

            var validationResult = _courseValidator.Validate(course);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_courseRepository.UpdateCourse(_mapper.Map<Course>(course)))
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
        public IActionResult SoftDeleteCourse(int id)
        {
           
            if (!_courseRepository.CourseExists(id))
                return NotFound();

            var courseToDelete = _courseRepository.GetCourse(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_courseRepository.SoftDeleteCourse(courseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting course");
            }
            return NoContent();
        }

        [HttpGet("{id}/prerequisites")]
        [ProducesResponseType(200, Type = typeof(List<CourseDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetPrerequisiteCourses(int id)
        {
          
            var prerequisiteCourses = _courseRepository.GetPrerequisiteCourses(id);

            if (prerequisiteCourses == null || prerequisiteCourses.Count == 0)
                return NotFound();

            var prerequisiteCoursesDTO = _mapper.Map<List<CourseDTO>>(prerequisiteCourses);

            return Ok(prerequisiteCoursesDTO);
        }

        [HttpGet("{id}/students")]
        [ProducesResponseType(200, Type = typeof(List<StudentDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetStudentsForCourse(int id)
        {

            var students = _courseRepository.GetStudentsForCourse(id);

            if (students == null || students.Count == 0)
                return NotFound();

            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);

            return Ok(studentsDTO);
        }
    }
}
