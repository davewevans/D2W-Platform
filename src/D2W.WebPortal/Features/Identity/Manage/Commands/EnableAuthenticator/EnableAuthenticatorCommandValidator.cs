namespace D2W.WebPortal.Features.Identity.Manage.Commands.EnableAuthenticator;

public class EnableAuthenticatorCommandValidator : AbstractValidator<EnableAuthenticatorCommand>
{
    #region Public Constructors

    public EnableAuthenticatorCommandValidator()
    {
        RuleFor(v => v.Code).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Code_is_required);
    }

    #endregion Public Constructors
}