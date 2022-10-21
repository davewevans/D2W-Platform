namespace D2W.WebPortal.Features.Identity.Roles.Commands.UpdateRole;

public class RoleForEditValidator : AbstractValidator<RoleForEdit>
{
    #region Public Constructors

    public RoleForEditValidator()
    {
        RuleFor(r => r.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Role_name_is_required);
    }

    #endregion Public Constructors
}