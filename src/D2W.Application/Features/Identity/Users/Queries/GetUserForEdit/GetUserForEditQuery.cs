namespace D2W.Application.Features.Identity.Users.Queries.GetUserForEdit;

public class GetUserForEditQuery : IRequest<Envelope<UserForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetUserForEditQueryHandler : IRequestHandler<GetUserForEditQuery, Envelope<UserForEdit>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetUserForEditQueryHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<UserForEdit>> Handle(GetUserForEditQuery request, CancellationToken cancellationToken)
        {
            return await _userUseCase.GetUser(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}