namespace D2W.Application.Features.Identity.Users.Queries.GetUserPermissions;

public class GetUserPermissionsQuery : IRequest<Envelope<UserPermissionsResponse>>
{
    #region Public Properties

    public string UserId { get; set; }
    public bool LoadingOnDemand { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, Envelope<UserPermissionsResponse>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetUserPermissionsQueryHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<UserPermissionsResponse>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _userUseCase.GetUserPermissions(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}