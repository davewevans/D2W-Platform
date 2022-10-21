namespace D2W.Application.Features.Identity.Permissions.Queries.GetPermissions;

public class GetPermissionsQuery : IRequest<Envelope<PermissionsResponse>>
{
    #region Public Properties

    public Guid? Id { get; set; }
    public bool LoadingOnDemand { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, Envelope<PermissionsResponse>>
    {
        #region Private Fields

        private readonly IPermissionUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetPermissionsQueryHandler(IPermissionUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<PermissionsResponse>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            if (request.LoadingOnDemand)
                return await _roleUseCase.GetLoadedOnDemandPermissions(request);

            return await _roleUseCase.GetLoadedOneShotPermissions();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}