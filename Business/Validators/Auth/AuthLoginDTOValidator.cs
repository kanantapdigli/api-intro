using Business.DTOs.Auth.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Auth
{
    public class AuthLoginDTOValidator : AbstractValidator<AuthLoginDTO>
    {
        public AuthLoginDTOValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
