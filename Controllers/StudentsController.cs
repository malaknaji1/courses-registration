using AutoMapper;
using courses_registration.DTO;
using courses_registration.Helpers;
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
    public class StudentsController : BaseController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<StudentDTO> _studentValidator;

        public StudentsController(IStudentRepository studentRepository , IMapper mapper, IValidator<StudentDTO> studentValidator, Localizer localizer): base(localizer)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _studentValidator = studentValidator;
        }
        [BasicAuthFilter()]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(StudentDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetStudent(int id)
        {
            if (!_studentRepository.StudentExists(id))
                return Response(HttpStatusCode.NotFound,"notFound");

            var student = _mapper.Map<StudentDTO>(_studentRepository.GetStudent(id));
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest,"",ModelState);

            return Ok(student);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStudent([FromBody] StudentDTO student)
        {
            if (student == null)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            var validationResult = _studentValidator.Validate(student);
            if (!validationResult.IsValid)
            {
                return Response(HttpStatusCode.BadRequest,"validationErrors",validationResult.Errors);
            }

            if (!_studentRepository.CreateStudent(_mapper.Map<Student>(student)))
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
        public IActionResult UpdateStudent(int id, [FromBody] StudentDTO student)
        {
            if (student == null || id != student.Id)
                return BadRequest(ModelState);

            if (!_studentRepository.StudentExists(id))
                return NotFound();

            var validationResult = _studentValidator.Validate(student);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_studentRepository.UpdateStudent(_mapper.Map<Student>(student)))
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
        public IActionResult SoftDeleteStudent(int id)
        {

            if (!_studentRepository.StudentExists(id))
                return NotFound();

            var studentToDelete = _studentRepository.GetStudent(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentRepository.SoftDeleteStudent(studentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting student");
            }
            return NoContent();
        }

        [HttpGet("{id}/courses")]
        [ProducesResponseType(200, Type = typeof(List<StudentDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetStudentsForCourse(int id)
        {

            var courses = _studentRepository.GetCoursesForStudent(id);

            if (courses == null || courses.Count == 0)
                return NotFound();

            var coursesDTO = _mapper.Map<List<CourseDTO>>(courses);

            return Ok(coursesDTO);
        }
    }
}
