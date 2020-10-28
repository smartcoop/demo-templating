using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTemplating.Api.Validator {
    public class LoginFormValidator : AbstractValidator<Dto.Login> {
        public LoginFormValidator() {
            RuleFor(x => x.Username).Cascade(CascadeMode.Stop).NotNull().Length(3, 50);
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotNull().Length(3, 50);
        }

    }
}
