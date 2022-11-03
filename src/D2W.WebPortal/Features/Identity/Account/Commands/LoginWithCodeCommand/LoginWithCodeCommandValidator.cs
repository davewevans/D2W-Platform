using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Identity.Account.Commands.LoginWithCodeCommand
{
    public class LoginWithCodeCommandValidator : AbstractValidator<LoginWithCodeCommand>
    {
        #region Public Constructors

        public LoginWithCodeCommandValidator()
        {
            RuleFor(v => v.TwoFactorCode).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(BackendResources.Resource.Two_factor_authentication_code_is_required)
                .MaximumLength(7).WithMessage(BackendResources.Resource.Two_factor_authentication_code_must_not_exceed_7_characters)
                .MinimumLength(6).WithMessage(BackendResources.Resource.Two_factor_authentication_code_must_be_at_least_6_character_long);
        }

        #endregion Public Constructors
    }
}