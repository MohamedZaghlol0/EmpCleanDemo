using FluentValidation;
using Microsoft.Extensions.Localization;
using MyAppApplication.Commands;
using MyAppApplication.Shared.Contracts;
using MyAppDomain.Constatns;

namespace MyAppInfrastructure.Validation
{
    public class AddEmpCommandValidator : AbstractValidator<AddEmpCommand>
    {
        // private readonly IStringLocalizer<AddEmpCommandValidator> _localizer;
        private readonly ILocalizationService _localizer;

        public AddEmpCommandValidator(ILocalizationService localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Emp.Name)
                .NotEmpty().WithMessage(localizer.GetLocalizedString(LocalizeKeys.EmpNameFieldIsRequired))
                .MinimumLength(0).WithMessage("Employee Name must not be empty.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Emp.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.Emp.Phone)
                .Matches(@"^(\+?[0-9]{1,3})?([0-9]{10,15})$").WithMessage("Invalid phone number format.")
                .When(x => !string.IsNullOrEmpty(x.Emp.Phone)); // Validate if provided
        }
    }
}