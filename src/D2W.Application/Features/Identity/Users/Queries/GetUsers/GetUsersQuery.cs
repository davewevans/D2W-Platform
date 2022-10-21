namespace D2W.Application.Features.Identity.Users.Queries.GetUsers;

public class GetUsersQuery : FilterableQuery, IRequest<Envelope<UsersResponse>>
{
    #region Public Properties

    public IList<string> SelectedRoleIds { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Envelope<UsersResponse>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetUsersQueryHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<UsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userUseCase.GetUsers(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}