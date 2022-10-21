namespace D2W.Application.Features.Identity.Manage.Commands.ConfirmEmailChange;

public class ConfirmEmailChangeCommand : IRequest<Envelope<ChangeEmailResponse>>
{
    #region Public Properties

    public string UserId { get; set; }
    public string Email { get; set; }
    public string Code { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class ConfirmEmailChangeCommandHandler : IRequestHandler<ConfirmEmailChangeCommand, Envelope<ChangeEmailResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ConfirmEmailChangeCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ChangeEmailResponse>> Handle(ConfirmEmailChangeCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.ConfirmEmailChange(request.UserId, request.Email, request.Code);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}