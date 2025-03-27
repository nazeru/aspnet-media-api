using System.Globalization;
using FluentValidation;
using MinimalApi.Core.Cqrs.Commands.Users;
using MinimalApi.Core.Entities;

namespace MinimalApi.Core.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(e => e.FirstName).MaximumLength(50);
        RuleFor(e => e.LastName).MaximumLength(50);
        RuleFor(e => e.Birthday);
        RuleFor(e => e.Email).EmailAddress();
        RuleFor(e => e.Phone).Matches(@"^\+?[78]?(9)(\d{9})$");
        RuleFor(e => e.Username).NotEmpty().MinimumLength(3).MaximumLength(20);
        // RuleFor(e => e.Password).NotEmpty().MinimumLength(6);
        // RuleFor(e => e.ConfirmPassword).NotEmpty().MinimumLength(6);
    }
}