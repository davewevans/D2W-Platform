namespace D2W.WebPortal.Features.Identity.Account.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    #region Public Constructors

    public LoginCommandValidator()
    {
        RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Username_is_required)
            .EmailAddress().WithMessage(v => string.Format(BackendResources.Resource.Username_is_invalid, v.Email));

        RuleFor(v => v.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Password_is_required);
    }

    #endregion Public Constructors
}