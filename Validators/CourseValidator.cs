using courses_registration.Data;
using courses_registration.DTO;
using courses_registration.Helpers;
using FluentValidation;

namespace courses_registration.Validators
{
    public class CourseValidator : AbstractValidator<CourseDTO>
    {
        private readonly Localizer _localizer;

        public CourseValidator(Localizer localizer)
        {
            _localizer = localizer;
            RuleFor(course => course.Name)
                .NotEmpty().WithMessage(_localizer.GetLocalized("courseNameRequired"))
                .MaximumLength(80).WithMessage(_localizer.GetLocalized("courseNameMaxLength"))
                .Matches("^[a-zA-Z0-9\\s]+$").WithMessage(_localizer.GetLocalized("courseNameFormat"));
            
        }

    }
}
