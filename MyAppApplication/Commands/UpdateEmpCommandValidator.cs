using FluentValidation;
using MyAppApplication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAppInfrastructure.Validation
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.PutEmp.Phone)
                .Matches(@"^(\+?[0-9]{1,3})?([0-9]{10,15})$")
                .WithMessage("Invalid phone number format.")
                .When(x => !string.IsNullOrEmpty(x.PutEmp.Phone));  

            RuleFor(x => x.PutEmp.Email)
                .EmailAddress()
                .WithMessage("Invalid email address format.")
                .When(x => !string.IsNullOrEmpty(x.PutEmp.Email)); 
        }
    }
}
