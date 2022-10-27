using DocumentFormat.OpenXml.Wordprocessing;

namespace D2W.Application.Features.Identity.Account.Commands.Register;

public class RegisterCommand : IRequest<Envelope<RegisterResponse>>
{
    #region Public Properties

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ReturnUrl { get; set; }
    public bool IsBetaUser { get; set; }
    public string HeardAboutUsFrom { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity()
    {
        var nameSplit = FullName?.Split(' ');
        string firstName = string.Empty;
        string lastName = string.Empty;
        if (nameSplit != null)
        {
            firstName = nameSplit[0];
            lastName = nameSplit[^1];
        }

        string[] defaultProfilePics = new[]
        {
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/2DDDE973-40EC-4004-ABC0-73FD4CD6D042-200w.jpeg",
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/2DDDE973-40EC-4004-ABC0-73FD4CD6D042-200w.jpeg",
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/344CFC24-61FB-426C-B3D1-CAD5BCBD3209-200w.jpeg",
            "https://elevateottstoragedev.blob.core.windows.net/elevate-ott-dev-image-container/852EC6E1-347C-4187-9D42-DF264CCF17BF-200w.jpeg"
        };

        var randomizer = new Random();

        return new()
        {
            UserName = Email,
            Email = Email,
            PhoneNumber = PhoneNumber,
            HeardAboutUsFrom = HeardAboutUsFrom,
            Name = firstName,
            Surname = lastName,
            AppUserType = IsBetaUser ? ApplicationUserType.BetaTester : ApplicationUserType.Designer
        };
    }

    #endregion Public Methods

    #region Public Classes

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Envelope<RegisterResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public RegisterCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.Register(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}