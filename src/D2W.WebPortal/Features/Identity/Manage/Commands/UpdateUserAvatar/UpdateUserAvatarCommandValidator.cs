namespace D2W.WebPortal.Features.Identity.Manage.Commands.UpdateUserAvatar;

public class CurrentUserForEditValidator : AbstractValidator<UserAvatarForEdit>
{
    #region Public Constructors

    public CurrentUserForEditValidator()
    {
        //RuleFor(v => v.AvatarUri).Cascade(CascadeMode.Stop)
        //    .NotEmpty().WithMessage(BackendResources.Resource.Profile_picture_is_required)
        //    .When(v => v.IsAvatarAdded == false);
    }

    #endregion Public Constructors
}