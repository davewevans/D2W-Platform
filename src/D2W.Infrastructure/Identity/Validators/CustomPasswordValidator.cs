namespace D2W.Infrastructure.Identity.Validators;

public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
{
    #region Public Methods

    public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
    {
        var username = await manager.GetUserNameAsync(user);

        if (username == password)
            return IdentityResult.Failed(new IdentityError
            {
                Code = "InvalidPassword",
                Description = Resource.Password_cannot_contain_username
            });

        return password.Contains("password") ? IdentityResult.Failed(new IdentityError
        {
            Code = "InvalidPassword",
            Description = Resource.Password_cannot_contain_password
        }) : IdentityResult.Success;
    }

    #endregion Public Methods
}