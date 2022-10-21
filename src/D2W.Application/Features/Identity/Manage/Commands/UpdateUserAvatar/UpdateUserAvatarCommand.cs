namespace D2W.Application.Features.Identity.Manage.Commands.UpdateUserAvatar;

public class UpdateUserAvatarCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public IFormFile Avatar { get; set; }
    public string AvatarUri { get; set; }
    public bool IsAvatarAdded { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class UpdateUserAvatarCommandHandler : IRequestHandler<UpdateUserAvatarCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateUserAvatarCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.UpdateUserAvatar(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}