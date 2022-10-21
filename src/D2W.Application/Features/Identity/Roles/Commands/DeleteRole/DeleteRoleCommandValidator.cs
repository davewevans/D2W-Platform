namespace D2W.Application.Features.Identity.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    #region Public Constructors

    public DeleteRoleCommandValidator()
    {
        RuleFor(v => v.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(Resource.Invalid_role_Id);
    }

    #endregion Public Constructors
}