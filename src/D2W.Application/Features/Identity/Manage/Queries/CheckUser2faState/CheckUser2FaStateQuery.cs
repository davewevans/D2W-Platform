namespace D2W.Application.Features.Identity.Manage.Queries.CheckUser2faState;

public class CheckUser2FaStateQuery : IRequest<Envelope<User2FaStateResponse>>
{
    #region Public Classes

    public class CheckUser2FaStateHandler : IRequestHandler<CheckUser2FaStateQuery, Envelope<User2FaStateResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CheckUser2FaStateHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<User2FaStateResponse>> Handle(CheckUser2FaStateQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.CheckUser2FaState();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}