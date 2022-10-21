namespace D2W.WebPortal.Features.Identity.Manage.Commands.UpdateUserAvatar;

public class UpdateUserAvatarCommand
{
    #region Public Properties

    public IFormFile Avatar { get; set; }
    public string AvatarUri { get; set; }
    public bool IsAvatarAdded { get; set; }

    #endregion Public Properties
}