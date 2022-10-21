namespace D2W.Application.Features.AppSettings.Commands.UpdateSettings;

public class UpdateTokenSettingsCommandValidator : AbstractValidator<UpdateTokenSettingsCommand>
{
    #region Public Constructors

    public UpdateTokenSettingsCommandValidator()
    {
        RuleFor(v => v.TokenSettings.AccessTokenTimeSpan).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Access_token_timespan_is_required);

        RuleFor(v => v.TokenSettings.RefreshTokenTimeSpan).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Refresh_token_timespan_cannot_be_null)
            .GreaterThan(v => v.TokenSettings.AccessTokenTimeSpan)
            .When(v => v.TokenSettings.AccessTokenTimeSpan != null)
            .WithMessage(Resource.Refresh_token_timespan_must_be_greater_than_access_token_expiry_time);
    }

    #endregion Public Constructors
}

public class TokenSettingsForEditValidator : AbstractValidator<TokenSettingsForEdit>
{
    #region Public Constructors

    public TokenSettingsForEditValidator()
    {
        RuleFor(v => v.TokenSettings.AccessTokenTimeSpan).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Access_token_timespan_is_required);

        RuleFor(v => v.TokenSettings.RefreshTokenTimeSpan).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Refresh_token_timespan_is_required)
            .GreaterThan(v => v.TokenSettings.AccessTokenTimeSpan)
            .When(v => v.TokenSettings.AccessTokenTimeSpan != null)
            .WithMessage(Resource.Refresh_token_timespan_must_be_greater_than_access_token_expiry_time);
    }

    #endregion Public Constructors
}