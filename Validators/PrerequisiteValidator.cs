using courses_registration.Data;
using courses_registration.DTO;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Validators
{
    public class PrerequisiteValidator: AbstractValidator<PrerequisiteDTO>
    {
        private readonly AppDbContext _context;

        public PrerequisiteValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(p => p.CourseId)
                .Must(BeValidCourseId)
                .WithMessage("CourseId must be a valid Course Id.");

            RuleFor(p => p.PrerequisiteId)
                .Must(BeValidCourseId)
                .WithMessage("PrerequisiteId must be a valid Course Id.");
            
        }

        private bool BeValidCourseId(int courseId)
        {
            return _context.Courses.Any(c => c.Id == courseId);
        }
    }
}
