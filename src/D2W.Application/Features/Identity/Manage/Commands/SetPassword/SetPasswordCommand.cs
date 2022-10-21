namespace D2W.Application.Features.Identity.Manage.Commands.SetPassword;

public class SetPasswordCommand : IRequest<Envelope<SetPasswordResponse>>
{
    #region Public Properties

    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, Envelope<SetPasswordResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public SetPasswordCommandHandler(IManageUseCase manageUseCase, IMediator mediator)
        {
            _manageUseCase = manageUseCase;
            _mediator = mediator;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<SetPasswordResponse>> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.SetPassword(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}