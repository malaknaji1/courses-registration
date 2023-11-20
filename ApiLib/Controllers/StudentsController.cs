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
                return Response(HttpStatusCode.NotFound,"studentNotFound");

            var student = _mapper.Map<StudentDTO>(_studentRepository.GetStudent(id));
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest,"",ModelState);

            return Response(HttpStatusCode.OK,"",student);
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
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return Response(HttpStatusCode.BadRequest,"validationErrors", validationErrors);
            }
            

            if (!_studentRepository.CreateStudent(_mapper.Map<Student>(student)))
                return Response(HttpStatusCode.InternalServerError, "wrongSaving");
            
            return Response(HttpStatusCode.OK, "successfullyCreated");
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStudent(int id, [FromBody] StudentDTO student)
        {
            if (student == null || id != student.Id)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            if (!_studentRepository.StudentExists(id))
                return Response(HttpStatusCode.NotFound,"studentNotFound");

            var validationResult = _studentValidator.Validate(student);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return Response(HttpStatusCode.BadRequest, "validationErrors", validationErrors);
            }

            if (!_studentRepository.UpdateStudent(_mapper.Map<Student>(student)))
                return Response(HttpStatusCode.InternalServerError, "wrongUpdating", ModelState);

            return Response(HttpStatusCode.OK, "successfullyUpdated");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult SoftDeleteStudent(int id)
        {

            if (!_studentRepository.StudentExists(id))
                return Response(HttpStatusCode.NotFound, "studentNotFound");

            var studentToDelete = _studentRepository.GetStudent(id);

            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            if (!_studentRepository.SoftDeleteStudent(studentToDelete))
                return Response(HttpStatusCode.InternalServerError, "wrongDeleting");

            return Response(HttpStatusCode.OK, "successfullyDeleted");
        }

        [HttpGet("{id}/courses")]
        [ProducesResponseType(200, Type = typeof(List<StudentDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetCoursesForStudent(int id)
        {

            var courses = _studentRepository.GetCoursesForStudent(id);

            if (courses == null || courses.Count == 0)
                return Response(HttpStatusCode.NotFound, "noCoursesFound");

            var coursesDTO = _mapper.Map<List<CourseDTO>>(courses);

            return Response(HttpStatusCode.OK,"",coursesDTO);
        }
    }
}
