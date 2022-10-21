namespace D2W.Application.Features.Identity.Manage.Commands.UpdateUserProfile;

public class UpdateUserProfileCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Name { get; set; }
    public string Surname { get; set; }
    public string JobTitle { get; set; }
    public string PhoneNumber { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity(ApplicationUser user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        user.Name = Name;
        user.Surname = Surname;
        user.JobTitle = JobTitle;
        user.PhoneNumber = PhoneNumber;

        return user;
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;
        private readonly IMediator _mediator;

        #endregion Private Fields

        #region Public Constructors

        public UpdateUserProfileCommandHandler(IManageUseCase manageUseCase, IMediator mediator)
        {
            _manageUseCase = manageUseCase;
            _mediator = mediator;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.UpdateUserProfile(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}