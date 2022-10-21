namespace D2W.WebPortal.Features.Identity.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    #region Public Constructors

    public DeleteRoleCommandValidator()
    {
        RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Invalid_role_Id);
    }

    #endregion Public Constructors
}