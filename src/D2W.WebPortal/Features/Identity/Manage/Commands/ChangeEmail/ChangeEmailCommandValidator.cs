namespace D2W.WebPortal.Features.Identity.Manage.Commands.ChangeEmail;

public class UpdateUserProfileCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    #region Public Constructors

    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.NewEmail).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Username_is_required)
            .EmailAddress().WithMessage(v => string.Format(BackendResources.Resource.Username_is_invalid, v.NewEmail));
    }

    #endregion Public Constructors
}