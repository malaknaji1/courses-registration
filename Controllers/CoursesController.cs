using AutoMapper;
using courses_registration.DTO;
using courses_registration.Helpers;
using courses_registration.Interfaces;
using courses_registration.Models;
using courses_registration.Resources;
using courses_registration.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace courses_registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BaseController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CourseDTO> _courseValidator;
        public CoursesController(ICourseRepository courseRepository, IMapper mapper, IValidator<CourseDTO> courseValidator, Localizer localizer) : base(localizer)
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
                return Response(HttpStatusCode.NotFound, "courseNotFound");

            var course =_mapper.Map<CourseDTO>( _courseRepository.GetCourse(id));
            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            return Response(HttpStatusCode.OK, "", course);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromBody] CourseDTO course)
        {
        

            if (course == null)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            if (_courseRepository.IsCourseNameExisits(course.Name))
                return Response(HttpStatusCode.Conflict, "courseExists");

            var validationResult = _courseValidator.Validate(course);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return Response(HttpStatusCode.BadRequest, "validationErrors", validationErrors);
            }

            if (!_courseRepository.CreateCourse(_mapper.Map<Course>(course)))
                return Response(HttpStatusCode.InternalServerError, "wrongSaving");

            return Response(HttpStatusCode.OK, "successfullyCreated");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int id, [FromBody] CourseDTO course)
        {
            if (course == null ||id != course.Id)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            if (!_courseRepository.CourseExists(id))
                return Response(HttpStatusCode.NotFound, "courseNotFound");

            if (_courseRepository.IsCourseNameExisits(course.Name))
                return Response(HttpStatusCode.Conflict, "courseExists");

            var validationResult = _courseValidator.Validate(course);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return Response(HttpStatusCode.BadRequest, "validationErrors", validationErrors);
            }

            if (!_courseRepository.UpdateCourse(_mapper.Map<Course>(course)))
                return Response(HttpStatusCode.InternalServerError, "wrongUpdating", ModelState);

            return Response(HttpStatusCode.OK, "successfullyUpdated");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult SoftDeleteCourse(int id)
        {
           
            if (!_courseRepository.CourseExists(id))
                return Response(HttpStatusCode.NotFound, "courseNotFound");

            var courseToDelete = _courseRepository.GetCourse(id);

            if (!ModelState.IsValid)
                return Response(HttpStatusCode.BadRequest, "", ModelState);

            if (!_courseRepository.SoftDeleteCourse(courseToDelete))
                return Response(HttpStatusCode.InternalServerError, "wrongDeleting");

            return Response(HttpStatusCode.OK, "successfullyDeleted");
        }

        [HttpGet("{id}/prerequisites")]
        [ProducesResponseType(200, Type = typeof(List<CourseDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetPrerequisiteCourses(int id)
        {
          
            var prerequisiteCourses = _courseRepository.GetPrerequisiteCourses(id);

            if (prerequisiteCourses == null || prerequisiteCourses.Count == 0)
                return Response(HttpStatusCode.NotFound, "notFound");

            var prerequisiteCoursesDTO = _mapper.Map<List<CourseDTO>>(prerequisiteCourses);

            return Response(HttpStatusCode.OK, "", prerequisiteCoursesDTO);
        }

        [HttpGet("{id}/students")]
        [ProducesResponseType(200, Type = typeof(List<StudentDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetStudentsForCourse(int id)
        {

            var students = _courseRepository.GetStudentsForCourse(id);

            if (students == null || students.Count == 0)
                return Response(HttpStatusCode.NotFound, "notFound");

            var studentsDTO = _mapper.Map<List<StudentDTO>>(students);

            return Response(HttpStatusCode.OK, "", studentsDTO);
        }
    }
}
