namespace D2W.WebPortal.Features.AppSettings.Commands.UpdateSettings;

public class IdentitySettingsForEditValidator : AbstractValidator<IdentitySettingsForEdit>
{
    #region Public Constructors

    public IdentitySettingsForEditValidator()
    {
        RuleFor(v => v.UserSettings.AllowedUserNameCharacters).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Allowed_username_characters_are_required);
    }

    #endregion Public Constructors
}