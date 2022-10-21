namespace D2W.WebPortal.Extensions;
public static class UserExtensions
{
    public static bool HasPermission(this ClaimsPrincipal claimsPrincipal, string permission)
    {
        var claims = claimsPrincipal.Claims;
        var permissions = claims.Where(c => c.Type == "permissions").SelectMany(claim => claim.Value.Filter(new List<char>() {'[', '"', ']'}).Split(',')).ToList();
        return permissions.Any(p => p == permission);
    }
}
