using D2W.Application.Common.Managers;

namespace D2W.Application.UseCases.Identity;

public class ManageUseCase : IManageUseCase
{
    #region Private Fields

    private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly ApplicationUserManager _userManager;

    private readonly ILogger<RegisterCommand> _logger;

    private readonly INotificationService _notificationService;

    private readonly IAccountUseCase _accountUseCase;

    private readonly ITokenGeneratorService _tokenGeneratorService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IConfigReaderService _configReaderService;

    private readonly IStorageProvider _storageProvider;

    private readonly IDemoUserPasswordSetterService _demoUserPasswordSetterService;

    private readonly UrlEncoder _urlEncoder;

    #endregion Private Fields

    #region Public Constructors

    public ManageUseCase(ApplicationUserManager userManager,
                         SignInManager<ApplicationUser> signInManager,
                         ILogger<RegisterCommand> logger,
                         UrlEncoder urlEncoder,
                         INotificationService notificationService,
                         IAccountUseCase accountUseCase,
                         ITokenGeneratorService tokenGeneratorService,
                         IHttpContextAccessor httpContextAccessor,
                         IConfigReaderService configReaderService,
                         IStorageProvider storageProvider,
                         IDemoUserPasswordSetterService demoUserPasswordSetterService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _urlEncoder = urlEncoder;
        _notificationService = notificationService;
        _accountUseCase = accountUseCase;
        _tokenGeneratorService = tokenGeneratorService;
        _httpContextAccessor = httpContextAccessor;
        _configReaderService = configReaderService;
        _storageProvider = storageProvider;
        _demoUserPasswordSetterService = demoUserPasswordSetterService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<CurrentUserForEdit>> GetCurrentUser()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<CurrentUserForEdit>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<CurrentUserForEdit>.Result.Unauthorized(Resource.Unable_to_load_user);

        var payload = new CurrentUserForEdit
        {
            Username = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Name = user.Name,
            Surname = user.Surname,
            JobTitle = user.JobTitle,
            AvatarUri = user.AvatarUri
        };

        return Envelope<CurrentUserForEdit>.Result.Ok(payload);
    }

    public async Task<Envelope<UserAvatarForEdit>> GetUserAvatar()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<UserAvatarForEdit>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<UserAvatarForEdit>.Result.Unauthorized(Resource.Unable_to_load_user);

        var payload = new UserAvatarForEdit
        {
            AvatarUri = user.AvatarUri
        };

        return Envelope<UserAvatarForEdit>.Result.Ok(payload);
    }

    public async Task<Envelope<string>> UpdateUserAvatar(UpdateUserAvatarCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<string>.Result.Unauthorized(Resource.Unable_to_load_user);

        var updateUserResult = await _userManager.UpdateWithFilesAsync(user, request.Avatar, request.AvatarUri, user.Email.ReplaceSpaceAndSpecialCharsWithDashes(), _storageProvider);

        return !updateUserResult.Succeeded
            ? Envelope<string>.Result.AddErrors(updateUserResult.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok(Resource.User_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> UpdateUserProfile(UpdateUserProfileCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<string>.Result.Unauthorized(Resource.Unable_to_load_user);

        request.MapToEntity(user);

        var updateUserResult = await _userManager.UpdateAsync(user);

        return !updateUserResult.Succeeded
            ? Envelope<string>.Result.AddErrors(updateUserResult.Errors.ToApplicationResult(), ResponseType.ServerError)
            : Envelope<string>.Result.Ok(Resource.User_has_been_updated_successfully);
    }

    public async Task<Envelope<ChangeEmailResponse>> ChangeEmail(ChangeEmailCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<ChangeEmailResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(_httpContextAccessor.GetUserId());

        if (user == null)
            return Envelope<ChangeEmailResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        var email = await _userManager.GetEmailAsync(user);

        ChangeEmailResponse payload;

        if (request.NewEmail == email)
        {
            payload = new ChangeEmailResponse
            {
                RequireConfirmedAccount = false,
                EmailUnchanged = true,
                EmailConfirmationUrl = null,
                AuthResponse = null,
                SuccessMessage = Resource.Your_email_is_unchanged
            };
            return Envelope<ChangeEmailResponse>.Result.Ok(payload);
        }

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
        {
            var callbackUrl = await _userManager.SendActivationEmailAsync(user, request.NewEmail, _configReaderService, _notificationService, _httpContextAccessor);

            var tokenResponse = await GenerateAuthResponseAsync(user);

            if (tokenResponse == null)
                return Envelope<ChangeEmailResponse>.Result.BadRequest(string.Format(Resource.value_cannot_be_null, nameof(tokenResponse)));

            if (request.DisplayConfirmAccountLink)
            {
                payload = new ChangeEmailResponse
                {
                    RequireConfirmedAccount = true,
                    DisplayConfirmAccountLink = request.DisplayConfirmAccountLink,
                    EmailConfirmationUrl = HttpUtility.UrlEncode(callbackUrl),
                    AuthResponse = tokenResponse,
                    SuccessMessage = Resource.Confirmation_link_to_change_email_has_been_sent
                };
                return Envelope<ChangeEmailResponse>.Result.Ok(payload);
            }

            payload = new ChangeEmailResponse
            {
                RequireConfirmedAccount = true,
                DisplayConfirmAccountLink = request.DisplayConfirmAccountLink,
                EmailConfirmationUrl = HttpUtility.UrlEncode(callbackUrl),
                SuccessMessage = Resource.Confirmation_link_to_change_email_has_been_sent,
            };

            return Envelope<ChangeEmailResponse>.Result.Ok(payload);
        }

        return await UpdateUserNameAndEmail(user, request.NewEmail);
    }

    public async Task<Envelope<ChangeEmailResponse>> ConfirmEmailChange(string userId, string email, string code)
    {
        if (string.IsNullOrEmpty(userId))
            return Envelope<ChangeEmailResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        ChangeEmailResponse payload;

        if (user == null)
        {
            // Don't reveal that the user does not exist
            payload = new ChangeEmailResponse
            {
                RequireConfirmedAccount = false,
                DisplayConfirmAccountLink = false,
                EmailConfirmationUrl = null,
                SuccessMessage = Resource.Your_email_has_been_changed_successfully,
            };

            return Envelope<ChangeEmailResponse>.Result.Ok(payload);
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

        var changeEmailResult = await _userManager.ChangeEmailAsync(user, email, code);

        if (!changeEmailResult.Succeeded)
            return Envelope<ChangeEmailResponse>.Result.AddErrors(changeEmailResult.Errors.ToApplicationResult(), ResponseType.ServerError);

        // In our UI email and user name are one and the same, so when we update the email we need
        // to update the user name.
        var setUserNameResult = await _userManager.SetUserNameAsync(user, email);

        if (!setUserNameResult.Succeeded)
            return Envelope<ChangeEmailResponse>.Result.ServerError(Resource.Error_changing_your_user_name);

        var authResponse = await GenerateAuthResponseAsync(user);

        payload = new ChangeEmailResponse
        {
            RequireConfirmedAccount = false,
            DisplayConfirmAccountLink = false,
            EmailConfirmationUrl = null,
            SuccessMessage = Resource.Your_email_has_been_changed_successfully,
            AuthResponse = authResponse
        };

        return Envelope<ChangeEmailResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<bool>> RequirePassword()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<bool>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        return user == null
            ? Envelope<bool>.Result.Unauthorized(Resource.Unable_to_load_user)
            : Envelope<bool>.Result.Ok(await _userManager.HasPasswordAsync(user));
    }

    public async Task<Envelope<ChangePasswordResponse>> ChangePassword(ChangePasswordCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<ChangePasswordResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<ChangePasswordResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        var identityResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        await _demoUserPasswordSetterService.ResetDefaultPassword(request.NewPassword, user);

        if (!identityResult.Succeeded)
            return Envelope<ChangePasswordResponse>.Result.AddErrors(identityResult.Errors.ToApplicationResult(), ResponseType.ServerError);

        await _signInManager.RefreshSignInAsync(user);
        _logger.LogInformation("User changed their password successfully.");

        var authResponse = await GenerateAuthResponseAsync(user);

        var changePasswordResponse = new ChangePasswordResponse
        {
            SuccessMessage = Resource.Your_password_has_been_changed,
            AuthResponse = authResponse
        };

        return Envelope<ChangePasswordResponse>.Result.Ok(changePasswordResponse);
    }

    public async Task<Envelope<string>> DeletePersonalData(DeletePersonalDataCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<string>.Result.Unauthorized(Resource.Unable_to_load_user);

        var requirePassword = await _userManager.HasPasswordAsync(user);

        if (requirePassword)
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return Envelope<string>.Result.ServerError(Resource.Incorrect_password);

        var identityResult = await _userManager.DeleteAsync(user);

        if (!identityResult.Succeeded)
            return Envelope<string>.Result.ServerError(string.Format(Resource.Unexpected_error_occurred_deleting_user_with_Id, user.Id));

        await _signInManager.SignOutAsync();

        _logger.LogInformation($"User with ID '{userId}' deleted themselves.");

        return Envelope<string>.Result.Ok(string.Format(Resource.User_with_Id_deleted, userId));
    }

    public async Task<Envelope<bool>> Has2FaEnabled()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<bool>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<bool>.Result.Unauthorized(Resource.Unable_to_load_user);

        if (!await _userManager.GetTwoFactorEnabledAsync(user))
            return Envelope<bool>.Result.ServerError(string.Format(Resource.Cannot_disable_2FA_for_user_with_Id, user.Id));

        return Envelope<bool>.Result.Ok(true);
    }

    public async Task<Envelope<string>> Disable2Fa()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<string>.Result.Unauthorized(Resource.Unable_to_load_user);

        var identityResult = await _userManager.SetTwoFactorEnabledAsync(user, false);

        if (!identityResult.Succeeded)
            return Envelope<string>.Result.ServerError(string.Format(Resource.Unexpected_error_occurred_disabling_2FA, user.Id));

        _logger.LogInformation($"User with ID '{user.Id}' has disabled 2Fa.");
        return Envelope<string>.Result.Ok(Resource.Two_factor_authentication_has_been_disabled);
    }

    public async Task<Envelope<DownloadPersonalDataResponse>> DownloadPersonalData()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<DownloadPersonalDataResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<DownloadPersonalDataResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", user.Id);

        // Only include personal data for download

        var personalDataProps = typeof(IdentityUser).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

        var personalData = personalDataProps.ToDictionary(p => p.Name, p => p.GetValue(user)?.ToString() ?? "null");

        var logins = await _userManager.GetLoginsAsync(user);

        foreach (var loginInfo in logins)
            personalData.Add($"{loginInfo.LoginProvider} external login provider key", loginInfo.ProviderKey);

        var username = user.Email.ReplaceSpaceAndSpecialCharsWithDashes();

        var payload = new DownloadPersonalDataResponse
        {
            PersonalData = JsonSerializer.SerializeToUtf8Bytes(personalData),
            FileName = $"PersonalData-{username}",
            ContentType = "application/json"
        };

        return Envelope<DownloadPersonalDataResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<LoadSharedKeyAndQrCodeUriResponse>> LoadSharedKeyAndQrCodeUri()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<LoadSharedKeyAndQrCodeUriResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<LoadSharedKeyAndQrCodeUriResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        // Load the authenticator key & QR code URI to display on the form
        var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);

        if (string.IsNullOrEmpty(unformattedKey))
        {
            await _userManager.ResetAuthenticatorKeyAsync(user);
            unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
        }

        var payload = new LoadSharedKeyAndQrCodeUriResponse
        {
            SharedKey = FormatKey(unformattedKey)
        };

        var email = await _userManager.GetEmailAsync(user);

        payload.AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey);

        return Envelope<LoadSharedKeyAndQrCodeUriResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<EnableAuthenticatorResponse>> EnableAuthenticator(EnableAuthenticatorCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<EnableAuthenticatorResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<EnableAuthenticatorResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        // Strip spaces and hypens
        var verificationCode = request.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

        var is2FaTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
            user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

        if (!is2FaTokenValid)
            return Envelope<EnableAuthenticatorResponse>.Result.ServerError(Resource.Verification_code_is_invalid);

        await _userManager.SetTwoFactorEnabledAsync(user, true);

        var payload = new EnableAuthenticatorResponse();

        if (await _userManager.CountRecoveryCodesAsync(user) == 0)
        {
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            payload.RecoveryCodes = recoveryCodes.ToArray();
            payload.ShowRecoveryCodes = true; //If true, RedirectToPage("./ShowRecoveryCodes")
        }
        else
        {
            payload.ShowRecoveryCodes = false; //If false, RedirectToPage("./TwoFactorAuthentication");
        }

        _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);
        payload.SuccessMessage = Resource.Your_authenticator_app_has_been_verified;

        return Envelope<EnableAuthenticatorResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<User2FaStateResponse>> CheckUser2FaState()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<User2FaStateResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<User2FaStateResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

        var user2FaStateResponse = new User2FaStateResponse
        {
            IsTwoFactorEnabled = isTwoFactorEnabled,
            StatusMessage = !isTwoFactorEnabled ? string.Format(Resource.Cannot_generate_recovery_codes, user.UserName) : string.Empty,
        };

        return Envelope<User2FaStateResponse>.Result.Ok(user2FaStateResponse);
    }

    public async Task<Envelope<GenerateRecoveryCodesResponse>> GenerateRecoveryCodes()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<GenerateRecoveryCodesResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<GenerateRecoveryCodesResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

        if (!isTwoFactorEnabled)
            return Envelope<GenerateRecoveryCodesResponse>.Result.ServerError(string.Format(Resource.Cannot_generate_recovery_codes, user.UserName));

        var payload = new GenerateRecoveryCodesResponse
        {
            RecoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10)
        };

        payload.RecoveryCodes = payload.RecoveryCodes.ToArray();

        _logger.LogInformation($"User with ID '{user.Id}' has generated new 2FA recovery codes.", user.Id);

        payload.StatusMessage = Resource.You_have_generated_new_recovery_codes;

        return Envelope<GenerateRecoveryCodesResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<ResetAuthenticatorResponse>> ResetAuthenticator()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<ResetAuthenticatorResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<ResetAuthenticatorResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        await _userManager.SetTwoFactorEnabledAsync(user, false);

        await _userManager.ResetAuthenticatorKeyAsync(user);

        _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

        var authResponse = await GenerateAuthResponseAsync(user);

        var payload = new ResetAuthenticatorResponse
        {
            StatusMessage = Resource.Your_authenticator_app_key_has_been_reset,
            AuthResponse = authResponse
        };

        return Envelope<ResetAuthenticatorResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<SetPasswordResponse>> SetPassword(SetPasswordCommand request)
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<SetPasswordResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<SetPasswordResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        var identityResult = await _userManager.AddPasswordAsync(user, request.NewPassword);

        if (!identityResult.Succeeded)
            return Envelope<SetPasswordResponse>.Result.AddErrors(identityResult.Errors.ToApplicationResult(), ResponseType.ServerError);

        var loginResponse = await _accountUseCase.Login(new LoginCommand
        {
            Email = user.Email,
            Password = request.NewPassword
        });

        var payload = new SetPasswordResponse
        {
            NewAccessToken = loginResponse.Payload.AuthResponse.AccessToken,
            StatusMessage = Resource.Your_password_has_been_set
        };

        return Envelope<SetPasswordResponse>.Result.Ok(payload);
    }

    public async Task<Envelope<Get2FaStateResponse>> GetTwoFactorAuthenticationState()
    {
        var userId = _httpContextAccessor.GetUserId();

        if (string.IsNullOrEmpty(userId))
            return Envelope<Get2FaStateResponse>.Result.BadRequest(Resource.Invalid_user_Id);

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return Envelope<Get2FaStateResponse>.Result.Unauthorized(Resource.Unable_to_load_user);

        var payload = new Get2FaStateResponse
        {
            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null,
            Is2FaEnabled = await _userManager.GetTwoFactorEnabledAsync(user),
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user)
        };

        return Envelope<Get2FaStateResponse>.Result.Ok(payload);
    }

    #endregion Public Methods

    #region Private Methods

    private static string FormatKey(string unformattedKey)
    {
        var result = new StringBuilder();
        var currentPosition = 0;

        while (currentPosition + 4 < unformattedKey.Length)
        {
            result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
            currentPosition += 4;
        }

        if (currentPosition < unformattedKey.Length)
            result.Append(unformattedKey.Substring(currentPosition));

        return result.ToString().ToLowerInvariant();
    }

    private string GenerateQrCodeUri(string email, string unformattedKey)
    {
        return string.Format(AuthenticatorUriFormat, _urlEncoder.Encode("D2W"), _urlEncoder.Encode(email), unformattedKey);
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(ApplicationUser user)
    {
        var accessToken = await _tokenGeneratorService.GenerateAccessTokenAsync(user);
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();

        var authResponse = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return authResponse;
    }

    private async Task<Envelope<ChangeEmailResponse>> UpdateUserNameAndEmail(ApplicationUser user, string email)
    {
        user.Email = email;
        var updateUserResult = await _userManager.UpdateAsync(user);

        if (!updateUserResult.Succeeded)
            return Envelope<ChangeEmailResponse>.Result.AddErrors(updateUserResult.Errors.ToApplicationResult(), ResponseType.ServerError);

        // In our UI email and user name are one and the same, so when we update the email we need
        // to update the user name.

        var setUserNameResult = await _userManager.SetUserNameAsync(user, email);

        if (!setUserNameResult.Succeeded)
            return Envelope<ChangeEmailResponse>.Result.ServerError(Resource.Error_changing_user_name);

        var authResponse = await GenerateAuthResponseAsync(user);

        var changeEmailResponse = new ChangeEmailResponse
        {
            RequireConfirmedAccount = false,
            DisplayConfirmAccountLink = false,
            EmailConfirmationUrl = null,
            SuccessMessage = Resource.Your_email_has_been_successfully_changed,
            AuthResponse = authResponse
        };

        return Envelope<ChangeEmailResponse>.Result.Ok(changeEmailResponse);
    }

    #endregion Private Methods
}