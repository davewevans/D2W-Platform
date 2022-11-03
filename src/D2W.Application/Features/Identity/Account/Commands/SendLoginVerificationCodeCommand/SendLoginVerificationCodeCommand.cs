namespace D2W.Application.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand;

public class SendLoginVerificationCodeCommand : IRequest<Envelope<SendLoginVerificationCodeResponse>>
{
    #region Public Properties

    public string Email { get; set; }
    public bool RememberMachine { get; set; }
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }
    public string Provider { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetLoginVerificationCommandHandler : IRequestHandler<SendLoginVerificationCodeCommand, Envelope<SendLoginVerificationCodeResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetLoginVerificationCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<SendLoginVerificationCodeResponse>> Handle(SendLoginVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.SendLoginVerificationCode(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
