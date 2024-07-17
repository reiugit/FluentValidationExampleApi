using FluentValidation;
using FluentValidationExampleApi.Requests;

namespace FluentValidationExampleApi.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        const string AllLettersMessage = "{PropertyName} should be all letters.  ({PropertyName} '{PropertyValue}' is invalid.)";
        const string RequiredMessage = "{PropertyName} is required.";

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(RequiredMessage)
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
            .Must(AllLetters).WithMessage(AllLettersMessage);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(RequiredMessage)
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
            .Must(AllLetters).WithMessage(AllLettersMessage);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(RequiredMessage)
            .EmailAddress().WithMessage("Email '{PropertyValue}' is not valid.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(RequiredMessage)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.  (Password '{PropertyValue}' is too short.)")
            .MaximumLength(50).WithMessage("Password must not exceed 50 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Passwords do not match.");
    }

    bool AllLetters(string value)
    {
        return value.All(char.IsLetter);
    }
}
