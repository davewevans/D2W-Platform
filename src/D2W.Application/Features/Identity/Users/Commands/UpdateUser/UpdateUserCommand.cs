namespace D2W.Application.Features.Identity.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public IFormFile Avatar { get; set; }
    public string AvatarUri { get; set; }
    public bool IsAvatarAdded { get; set; }
    public IList<IFormFile> Attachments { get; set; }
    public int NumberOfAttachments { get; set; } = 0;
    public string Name { get; set; }
    public string Surname { get; set; }
    public string JobTitle { get; set; }
    public string PhoneNumber { get; set; }
    public bool SetRandomPassword { get; set; }
    public bool MustSendActivationEmail { get; set; }
    public bool IsSuspended { get; set; }

    public IList<string> AssignedRoleIds { get; set; }
    public IList<Guid> AttachmentIds { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity(ApplicationUser user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        user.UserName = Email;
        user.Email = Email;
        user.Name = Name;
        user.Surname = Surname;
        user.JobTitle = JobTitle;
        user.PhoneNumber = PhoneNumber;
        user.IsSuspended = IsSuspended;

        return user;
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateUserCommandHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userUseCase.EditUser(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}