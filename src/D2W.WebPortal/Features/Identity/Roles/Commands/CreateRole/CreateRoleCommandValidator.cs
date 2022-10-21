namespace D2W.WebPortal.Features.Identity.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    #region Public Constructors

    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(BackendResources.Resource.Role_name_is_required);
    }

    #endregion Public Constructors
}