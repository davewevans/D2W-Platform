namespace D2W.Application.Features.Identity.Users.Commands.GrantOrRevokeUserPermissions;

public class GrantOrRevokeUserPermissionsCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string UserId { get; set; }
    public IList<Guid> SelectedPermissionIds { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GrantOrRevokeUserPermissionsCommandHandler : IRequestHandler<GrantOrRevokeUserPermissionsCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GrantOrRevokeUserPermissionsCommandHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(GrantOrRevokeUserPermissionsCommand request, CancellationToken cancellationToken)
        {
            return await _userUseCase.GrantOrRevokeUserPermissions(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}