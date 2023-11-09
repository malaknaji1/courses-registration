using courses_registration.Data;
using courses_registration.DTO;
using FluentValidation;

namespace courses_registration.Validators
{
    public class CourseValidator : AbstractValidator<CourseDTO>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Name)
                .NotEmpty().WithMessage("Course name is required.")
                .MaximumLength(80).WithMessage("Course name cannot exceed 80 characters.")
                .Matches("^[a-zA-Z0-9\\s]+$").WithMessage("Course name can only contain letters, numbers, and spaces.");
        }

    }
}
