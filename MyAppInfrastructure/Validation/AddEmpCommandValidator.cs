using FluentValidation;
using MyAppApplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppInfrastructure.Validation
{
    public class AddEmpCommandValidator : AbstractValidator<AddEmpCommand>
    {
        public AddEmpCommandValidator()
        {
            RuleFor(x => x.Emp.Name)
                .NotEmpty().WithMessage("Name is required.")
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
