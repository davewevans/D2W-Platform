using D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;

public class RegisterWorkroomCommand : IRequest<Envelope<RegisterWorkroomResponse>>
{
    #region Public Properties

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

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

        // TODO remove for production

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
            Name = firstName,
            Surname = lastName,
            AppUserType = ApplicationUserType.Workroom,
            AvatarUri = defaultProfilePics[randomizer.Next(defaultProfilePics.Length)]
        };
    }

    #endregion Public Methods

    #region Public Classes

    public class RegisterWorkroomCommandHandler : IRequestHandler<RegisterWorkroomCommand, Envelope<RegisterWorkroomResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public RegisterWorkroomCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<RegisterWorkroomResponse>> Handle(RegisterWorkroomCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.RegisterWorkroom(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
