namespace D2W.Application.Features.AppSettings.Commands.UpdateSettings;

public class UpdateIdentitySettingsCommandValidator : AbstractValidator<UpdateIdentitySettingsCommand>
{
    #region Public Constructors

    public UpdateIdentitySettingsCommandValidator()
    {
        RuleFor(v => v.UserSettings.AllowedUserNameCharacters).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Allowed_username_characters_are_required);
    }

    #endregion Public Constructors
}

public class IdentitySettingsForEditValidator : AbstractValidator<IdentitySettingsForEdit>
{
    #region Public Constructors

    public IdentitySettingsForEditValidator()
    {
        RuleFor(v => v.UserSettings.AllowedUserNameCharacters).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Allowed_username_characters_are_required);
    }

    #endregion Public Constructors
}