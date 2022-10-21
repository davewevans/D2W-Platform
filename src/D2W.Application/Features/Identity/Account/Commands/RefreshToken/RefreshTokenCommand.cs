namespace D2W.Application.Features.Identity.Account.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<Envelope<AuthResponse>>
{
    #region Public Properties

    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Envelope<AuthResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public RefreshTokenCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.RefreshToken(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}