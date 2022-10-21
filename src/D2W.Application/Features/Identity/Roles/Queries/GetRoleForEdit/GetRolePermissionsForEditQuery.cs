namespace D2W.Application.Features.Identity.Roles.Queries.GetRoleForEdit;

public class GetRolePermissionsForEditQuery : IRequest<Envelope<RolePermissionsForEdit>>
{
    #region Public Properties

    public string RoleId { get; set; }
    public Guid? PermissionId { get; set; }
    public bool LoadingOnDemand { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetRolePermissionsForEditQueryHandler : IRequestHandler<GetRolePermissionsForEditQuery, Envelope<RolePermissionsForEdit>>
    {
        #region Private Fields

        private readonly IRoleUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetRolePermissionsForEditQueryHandler(IRoleUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<RolePermissionsForEdit>> Handle(GetRolePermissionsForEditQuery request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.GetRolePermissions(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}