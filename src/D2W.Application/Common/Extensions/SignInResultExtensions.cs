namespace D2W.Application.Common.Extensions;

public static class SignInResultExtensions
{
    #region Public Methods

    public static Dictionary<string, string> ToApplicationResult(this SignInResult signInResult)
    {
        var keyValuePairs = new Dictionary<string, string>();

        if (signInResult.IsLockedOut)
        {
            keyValuePairs.Add(nameof(signInResult.IsLockedOut), Resource.You_are_locked_out);
        }

        if (signInResult.IsNotAllowed)
        {
            keyValuePairs.Add(nameof(signInResult.IsNotAllowed), Resource.Please_confirm_your_email);
        }

        if (signInResult.RequiresTwoFactor)
        {
            keyValuePairs.Add(nameof(signInResult.RequiresTwoFactor), Resource.Two_factor_authentication_required);
        }

        if (!signInResult.Succeeded)
        {
            keyValuePairs.Add("Failed", Resource.Invalid_login_attempt);
        }
        return keyValuePairs;
    }

    public static string ToSerializedResult(this SignInResult signInResult)
    {
        var errorBuilder = new StringBuilder();

        if (signInResult.IsLockedOut)
        {
            errorBuilder.AppendLine(Resource.You_are_locked_out);

            errorBuilder.AppendLine("||");
        }

        if (signInResult.IsNotAllowed)
        {
            errorBuilder.AppendLine(Resource.Please_confirm_your_email);

            errorBuilder.AppendLine("||");
        }

        if (signInResult.RequiresTwoFactor)
        {
            errorBuilder.AppendLine(Resource.Two_factor_authentication_required);

            errorBuilder.AppendLine("||");
        }

        if (signInResult.Succeeded)
            return errorBuilder.ToString();

        errorBuilder.AppendLine(Resource.Invalid_login_attempt);

        errorBuilder.AppendLine("||");

        return errorBuilder.ToString();
    }

    #endregion Public Methods
}