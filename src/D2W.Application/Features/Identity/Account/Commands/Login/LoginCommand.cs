namespace D2W.Application.Features.Identity.Account.Commands.Login;

public class LoginCommand : IRequest<Envelope<LoginResponse>>
{
    #region Public Properties

    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Envelope<LoginResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public LoginCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.Login(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}