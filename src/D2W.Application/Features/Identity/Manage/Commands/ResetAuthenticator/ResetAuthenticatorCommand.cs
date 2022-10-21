namespace D2W.Application.Features.Identity.Manage.Commands.ResetAuthenticator;

public class ResetAuthenticatorCommand : IRequest<Envelope<ResetAuthenticatorResponse>>
{
    #region Public Classes

    public class ConfirmEmailCommandHandler : IRequestHandler<ResetAuthenticatorCommand, Envelope<ResetAuthenticatorResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ConfirmEmailCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ResetAuthenticatorResponse>> Handle(ResetAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.ResetAuthenticator();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}