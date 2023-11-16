using courses_registration.DTO;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace courses_registration.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator() 
        {

            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Username is required")
                .Length(5, 20).WithMessage("Username must be between 5 and 20 characters");

            RuleFor(user => user.PasswordHash)
               .NotEmpty().WithMessage("PasswordHash is required")
               .Must(password => password.Length >= 8 &&
                               password.Any(char.IsUpper) &&
                               password.Any(char.IsDigit) &&
                               password.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage("Invalid password format");

            RuleFor(user => user.UserTypeId)
                .NotEmpty().WithMessage("UserTypeId is required");

        }

    }
}
