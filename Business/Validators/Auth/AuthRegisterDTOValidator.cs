using Business.DTOs.Auth.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Auth
{
    public class AuthRegisterDTOValidator : AbstractValidator<AuthRegisterDTO>
    {
        public AuthRegisterDTOValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Istifadəçi adının daxil edilməsi vacibdir");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email-in daxil edilməsi vacibdir");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("Şifrənin uzunluğu minimum 8 simvol olmalıdır");
        }
    }
}
