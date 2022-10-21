namespace D2W.Application.Features.Identity.Manage.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<Envelope<ChangePasswordResponse>>
{
    #region Public Properties

    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Envelope<ChangePasswordResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public ChangePasswordCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ChangePasswordResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.ChangePassword(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}