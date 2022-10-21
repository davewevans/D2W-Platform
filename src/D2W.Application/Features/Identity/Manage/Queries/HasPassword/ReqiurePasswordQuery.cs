namespace D2W.Application.Features.Identity.Manage.Queries.HasPassword;

public class RequirePasswordQuery : IRequest<Envelope<bool>>
{
    #region Public Classes

    public class RequirePasswordQueryHandler : IRequestHandler<RequirePasswordQuery, Envelope<bool>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public RequirePasswordQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<bool>> Handle(RequirePasswordQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.RequirePassword();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}