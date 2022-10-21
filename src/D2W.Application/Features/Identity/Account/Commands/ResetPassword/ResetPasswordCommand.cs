namespace D2W.Application.Features.Identity.Account.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Code { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ResetPasswordCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.ResetPassword(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}