namespace D2W.Application.Features.Identity.Manage.Commands.EnableAuthenticator;

public class EnableAuthenticatorCommand : IRequest<Envelope<EnableAuthenticatorResponse>>
{
    #region Public Properties

    public string Code { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class EnableAuthenticatorCommandHandler : IRequestHandler<EnableAuthenticatorCommand, Envelope<EnableAuthenticatorResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public EnableAuthenticatorCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<EnableAuthenticatorResponse>> Handle(EnableAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.EnableAuthenticator(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}