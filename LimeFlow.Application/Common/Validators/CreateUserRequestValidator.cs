using FluentValidation;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.email).NotEmpty().Must(Functions.IsValidEmailAddress).WithMessage("Invalid email address");

            RuleFor(x => x.name).NotEmpty().Length(3, 100).WithMessage("Name cannot be null");
        }
    }
}
