using courses_registration.DTO;
using FluentValidation;

namespace courses_registration.Validators
{
    public class StudentValidator : AbstractValidator<StudentDTO>
    {
        public StudentValidator()
        {
            RuleFor(student => student.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(30).WithMessage("First name cannot exceed 30 characters")
                .Matches("^[a-zA-Z\\s]*$").WithMessage("First name can only contain English characters and spaces");

            RuleFor(student => student.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(30).WithMessage("Last name cannot exceed 30 characters")
                .Matches("^[a-zA-Z\\s]*$").WithMessage("Last name can only contain English characters and spaces");

            RuleFor(student => student.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(70).WithMessage("Email cannot exceed 70 characters.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(student => student.City)
                .MaximumLength(30).WithMessage("City cannot exceed 30 characters.")
                .Matches(@"^[a-zA-Z\s]*$").WithMessage("City can only contain English characters and spaces.");

            RuleFor(student => student.Mobile)
                .MaximumLength(14).WithMessage("Mobile cannot exceed 14 characters.")
                .Matches(@"^\+?[0-9]*$").WithMessage("Mobile can only contain '+' and numbers.");

            RuleFor(student => student.Major)
                .NotEmpty().WithMessage("Major is required.")
                .MaximumLength(50).WithMessage("Major cannot exceed 50 characters.");

        }
    }
}
