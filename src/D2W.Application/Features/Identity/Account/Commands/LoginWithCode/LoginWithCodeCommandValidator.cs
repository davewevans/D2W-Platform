using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.LoginWithCode;
public class LoginWithCodeCommandValidator : AbstractValidator<LoginWithCodeCommand>
{
    public LoginWithCodeCommandValidator()
    {
        RuleFor(v => v.TwoFactorCode).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Two_factor_authentication_code_is_required)
            .MaximumLength(7).WithMessage(Resource.Two_factor_authentication_code_must_not_exceed_7_characters)
            .MinimumLength(6).WithMessage(Resource.Two_factor_authentication_code_must_be_at_least_6_character_long);
    }
}
