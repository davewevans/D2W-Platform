namespace D2W.Application.Features.Identity.Manage.Queries.Get2faState;

public class Get2FaStateQuery : IRequest<Envelope<Get2FaStateResponse>>
{
    #region Public Classes

    public class Get2FaStateQueryHandler : IRequestHandler<Get2FaStateQuery, Envelope<Get2FaStateResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public Get2FaStateQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<Get2FaStateResponse>> Handle(Get2FaStateQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.GetTwoFactorAuthenticationState();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}