namespace D2W.Application.Features.Identity.Account.Commands.ResendEmailConfirmation;

public class ResendEmailConfirmationCommand : IRequest<Envelope<ResendEmailConfirmationResponse>>
{
    #region Public Properties

    public string Email { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class ResendEmailConfirmationCommandHandler : IRequestHandler<ResendEmailConfirmationCommand, Envelope<ResendEmailConfirmationResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ResendEmailConfirmationCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ResendEmailConfirmationResponse>> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.ResendEmailConfirmation(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}