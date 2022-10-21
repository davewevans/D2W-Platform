namespace D2W.Application.Features.Identity.Account.Commands.LoginWith2fa;

public class LoginWith2FaCommand : IRequest<Envelope<LoginWith2FaResponse>>
{
    #region Public Properties

    public string UserName { get; set; }
    public string TwoFactorCode { get; set; }
    public bool RememberMachine { get; set; }
    public bool RememberMe { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class LoginWith2FaCommandHandler : IRequestHandler<LoginWith2FaCommand, Envelope<LoginWith2FaResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public LoginWith2FaCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<LoginWith2FaResponse>> Handle(LoginWith2FaCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.LoginWith2Fa(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}