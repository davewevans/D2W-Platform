namespace D2W.Application.Features.Identity.Manage.Queries.GenerateRecoveryCodes;

public class GenerateRecoveryCodesQuery : IRequest<Envelope<GenerateRecoveryCodesResponse>>
{
    #region Public Classes

    public class GenerateRecoveryCodesQueryHandler : IRequestHandler<GenerateRecoveryCodesQuery, Envelope<GenerateRecoveryCodesResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GenerateRecoveryCodesQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<GenerateRecoveryCodesResponse>> Handle(GenerateRecoveryCodesQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.GenerateRecoveryCodes();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}