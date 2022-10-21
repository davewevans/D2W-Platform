namespace D2W.Application.Features.Identity.Manage.Commands.ChangeEmail;

public class ChangeEmailCommand : IRequest<Envelope<ChangeEmailResponse>>
{
    #region Public Properties

    public string NewEmail { get; set; }
    public bool DisplayConfirmAccountLink { get; set; } = true;

    #endregion Public Properties

    #region Public Classes

    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Envelope<ChangeEmailResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ChangeEmailCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ChangeEmailResponse>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.ChangeEmail(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}