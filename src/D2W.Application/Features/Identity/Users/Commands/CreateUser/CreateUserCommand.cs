namespace D2W.Application.Features.Identity.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Envelope<CreateUserResponse>>
{
    #region Public Properties

    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public IFormFile Avatar { get; set; }
    public string AvatarUri { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public int NumberOfAttachments { get; set; }
    public bool IsAvatarAdded { get; set; }
    public bool SetRandomPassword { get; set; }
    public bool MustSendActivationEmail { get; set; }
    public bool IsSuspended { get; set; }
    public bool IsStatic { get; set; }
    public IList<string> AssignedRoleIds { get; set; }
    public IList<IFormFile> Attachments { get; set; }
    public string JobTitle { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity()
    {
        var user = new ApplicationUser
        {
            UserName = Email,
            Email = Email,
            Name = Name,
            Surname = Surname,
            JobTitle = JobTitle,
            PhoneNumber = PhoneNumber,
            IsSuspended = IsSuspended,
            IsStatic = IsStatic,
        };
        return user;
    }

    #endregion Public Methods

    #region Public Classes

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Envelope<CreateUserResponse>>
    {
        #region Private Fields

        private readonly IUserUseCase _userUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CreateUserCommandHandler(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userUseCase.AddUser(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}