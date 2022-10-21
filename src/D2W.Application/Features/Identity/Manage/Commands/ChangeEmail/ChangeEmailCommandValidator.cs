namespace D2W.Application.Features.Identity.Manage.Commands.ChangeEmail;

public class UpdateUserProfileCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    #region Public Constructors

    public UpdateUserProfileCommandValidator()
    {
        RuleFor(v => v.NewEmail).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Username_is_required)
            .EmailAddress().WithMessage(v => string.Format(Resource.Username_is_invalid, v.NewEmail));
    }

    #endregion Public Constructors
}