namespace D2W.Application.Features.Identity.Account.Commands.LoginWithRecoveryCode;

public class LoginWithRecoveryCodeCommand : IRequest<Envelope<LoginWithRecoveryCodeResponse>>
{
    #region Public Properties

    public string RecoveryCode { get; set; }
    public string UserName { get; set; }
    public string ReturnUrl { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class LoginWithRecoveryCodeCommandHandler : IRequestHandler<LoginWithRecoveryCodeCommand, Envelope<LoginWithRecoveryCodeResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public LoginWithRecoveryCodeCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<LoginWithRecoveryCodeResponse>> Handle(LoginWithRecoveryCodeCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.LoginWithRecoveryCode(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}