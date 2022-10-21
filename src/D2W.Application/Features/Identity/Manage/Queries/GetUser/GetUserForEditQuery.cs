namespace D2W.Application.Features.Identity.Manage.Queries.GetUser;

public class GetCurrentUserForEditQuery : IRequest<Envelope<CurrentUserForEdit>>
{
    #region Public Classes

    public class GetCurrentUserForEditQueryHandler : IRequestHandler<GetCurrentUserForEditQuery, Envelope<CurrentUserForEdit>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetCurrentUserForEditQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CurrentUserForEdit>> Handle(GetCurrentUserForEditQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.GetCurrentUser();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}