namespace D2W.Application.Features.Identity.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public IList<Guid> SelectedPermissionIds { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void MapToEntity(ApplicationRole role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        role.Name = Name;
        role.IsDefault = IsDefault;
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IRoleUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateRoleCommandHandler(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.EditRole(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}