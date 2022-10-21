namespace D2W.Application.Features.Identity.Account.Commands.ForgotPassword;

public class ForgetPasswordCommand : IRequest<Envelope<ForgetPasswordResponse>>
{
    #region Public Properties

    public string Email { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, Envelope<ForgetPasswordResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ForgotPasswordCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ForgetPasswordResponse>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.ForgotPassword(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}