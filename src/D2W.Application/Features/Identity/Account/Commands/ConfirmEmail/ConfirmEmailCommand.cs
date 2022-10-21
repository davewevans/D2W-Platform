namespace D2W.Application.Features.Identity.Account.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string UserId { get; set; }
    public string Code { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ConfirmEmailCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.ConfirmEmail(request.UserId, request.Code);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}