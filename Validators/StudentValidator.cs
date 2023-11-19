using courses_registration.DTO;
using courses_registration.Helpers;
using FluentValidation;

namespace courses_registration.Validators
{
    public class StudentValidator : AbstractValidator<StudentDTO>
    {
        private readonly Localizer _localizer;

        public StudentValidator(Localizer localizer)
        {
            _localizer = localizer;
            RuleFor(student => student.FirstName)
               .NotEmpty().WithMessage(_localizer.GetLocalized("firstNameRequired"))
               .MaximumLength(30).WithMessage(_localizer.GetLocalized("firstNameMaxLength"))
               .Matches("^[a-zA-Z\\s]*$").WithMessage(_localizer.GetLocalized("firstNameFormat"));

            RuleFor(student => student.LastName)
                .NotEmpty().WithMessage(_localizer.GetLocalized("lastNameRequired"))
                .MaximumLength(30).WithMessage(_localizer.GetLocalized("lastNameMaxLength"))
                .Matches("^[a-zA-Z\\s]*$").WithMessage(_localizer.GetLocalized("lastNameFormat"));

            RuleFor(student => student.Email)
                .NotEmpty().WithMessage(_localizer.GetLocalized("emailRequired"))
                .MaximumLength(70).WithMessage(_localizer.GetLocalized("emailMaxLength"))
                .EmailAddress().WithMessage(_localizer.GetLocalized("emailFormat"));

            RuleFor(student => student.City)
                .MaximumLength(30).WithMessage(_localizer.GetLocalized("cityMaxLength"))
                .Matches(@"^[a-zA-Z\s]*$").WithMessage(_localizer.GetLocalized("cityFormat"));

            RuleFor(student => student.Mobile)
                .MaximumLength(14).WithMessage(_localizer.GetLocalized("mobileMaxLength"))
                .Matches(@"^\+?[0-9]*$").WithMessage(_localizer.GetLocalized("mobileFormat"));

            RuleFor(student => student.Major)
                .NotEmpty().WithMessage(_localizer.GetLocalized("majorRequired"))
                .MaximumLength(50).WithMessage(_localizer.GetLocalized("majorMaxLength"));

        }
    }
}
