using courses_registration.Data;
using courses_registration.DTO;
using FluentValidation;

namespace courses_registration.Validators
{
    public class EnrollmentValidator : AbstractValidator<EnrollmentDTO>
    {
        private readonly AppDbContext _context;
        public EnrollmentValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(p => p.CourseId)
                .Must(BeValidCourseId)
                .WithMessage("CourseId must be a valid Course Id.");

            RuleFor(p => p.StudentId)
                .Must(BeValidStudentId)
                .WithMessage("StudentId must be a valid Course Id.");

        }

        private bool BeValidCourseId(int courseId)
        {
            return _context.Courses.Any(c => c.Id == courseId);
        }
        private bool BeValidStudentId(int StudentId)
        {
            return _context.Students.Any(s => s.Id == StudentId);
        }

    }
}
