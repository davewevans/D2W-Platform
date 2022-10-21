namespace D2W.WebPortal.Features.Identity.Manage.Commands.SetPassword;

public class SetPasswordCommandValidator : AbstractValidator<SetPasswordCommand>
{
    #region Public Constructors

    public SetPasswordCommandValidator()
    {
        RuleFor(v => v.NewPassword).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.New_password_is_required)
            .MaximumLength(100).WithMessage(BackendResources.Resource.New_password_must_not_exceed_200_characters)
            .MinimumLength(6).WithMessage(BackendResources.Resource.New_password_must_be_at_least_6_characters);

        RuleFor(v => v.ConfirmPassword).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Confirm_password_is_required)
            .Equal(v => v.ConfirmPassword).WithMessage(BackendResources.Resource.The_password_and_confirmation_password_do_not_match);
    }

    #endregion Public Constructors
}