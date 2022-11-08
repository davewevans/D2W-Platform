using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Workrooms.Commands.UpdateWorkroom;

public class UpdateWorkroomCommand : IRequest<Envelope<string>>
{
    #region Public Properties
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ApplicationUserType AppUserType { get; set; }
    public string AvatarUri { get; set; }

    #endregion Public Properties

    #region Public Methods


    public void MapToEntity(ApplicationUser appUser)
    {
        if (appUser == null)
            throw new ArgumentNullException(nameof(appUser));

        var nameSplit = FullName?.Split(' ');
        string firstName = string.Empty;
        string lastName = string.Empty;
        if (nameSplit != null)
        {
            firstName = nameSplit[0];
            lastName = nameSplit[^1];
        }

        appUser.Name = firstName;
        appUser.Surname = lastName;
        appUser.Email = Email;
        appUser.PhoneNumber = PhoneNumber;
        appUser.AvatarUri = AvatarUri;
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateWorkroomCommandHandler : IRequestHandler<UpdateWorkroomCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IWorkroomUseCase _workroomUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateWorkroomCommandHandler(IWorkroomUseCase workroomUseCase)
        {
            _workroomUseCase = workroomUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateWorkroomCommand request, CancellationToken cancellationToken)
        {
            return await _workroomUseCase.EditWorkroom(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
