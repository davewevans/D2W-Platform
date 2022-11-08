namespace D2W.WebPortal.Features.Identity.Account.Commands.Register;

public class RegisterClientCommandValidator : AbstractValidator<RegisterClientCommand>
{
    #region Public Constructors

    public RegisterClientCommandValidator()
    {
        RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Username_is_required)
            .EmailAddress().WithMessage(v => string.Format(BackendResources.Resource.Username_is_invalid, v.Email))
            .MaximumLength(100).WithMessage(BackendResources.Resource.Username_must_not_exceed_200_characters)
            .MinimumLength(6).WithMessage(BackendResources.Resource.Username_must_be_at_least_6_characters);

    }

    #endregion Public Constructors
}