namespace D2W.Application.Features.Identity.Manage.Queries.GetUserAvatar;

public class GetUserAvatarForEditQuery : IRequest<Envelope<UserAvatarForEdit>>
{
    #region Public Classes

    public class GetUserAvatarQueryHandler : IRequestHandler<GetUserAvatarForEditQuery, Envelope<UserAvatarForEdit>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetUserAvatarQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<UserAvatarForEdit>> Handle(GetUserAvatarForEditQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.GetUserAvatar();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}