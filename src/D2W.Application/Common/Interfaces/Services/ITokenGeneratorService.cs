namespace D2W.Application.Common.Interfaces.Services;

public interface ITokenGeneratorService
{
    #region Public Methods

    Task<string> GenerateAccessTokenAsync(ApplicationUser user);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    #endregion Public Methods
}