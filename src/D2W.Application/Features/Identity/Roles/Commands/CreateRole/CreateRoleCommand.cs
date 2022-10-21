namespace D2W.Application.Features.Identity.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<Envelope<CreateRoleResponse>>
{
    #region Public Properties

    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public IList<Guid> SelectedPermissionIds { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationRole MapToEntity()
    {
        return new()
        {
            IsDefault = IsDefault,
            Name = Name
        };
    }

    #endregion Public Methods

    #region Public Classes

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Envelope<CreateRoleResponse>>
    {
        #region Private Fields

        private readonly IRoleUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CreateRoleCommandHandler(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateRoleResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.AddRole(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}