using D2W.Application.Common.Interfaces.UseCases;
using DocumentFormat.OpenXml.Wordprocessing;

namespace D2W.Application.Features.Workrooms.Commands.UpdateWorkroom;

public class UpdateWorkroomCommand : IRequest<Envelope<string>>
{
    #region Public Properties
    public string Id { get; set; }

    public string CompanyName { get; set; }
    public string EmailAddress { get; set; }
    public string AltEmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string AltPhoneNumber { get; set; }
    public string Fax { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public Guid? CountryId { get; set; }

    #endregion Public Properties

    #region Public Methods


    public void MapToEntity(ApplicationUser appUser)
    {
        if (appUser == null)
            throw new ArgumentNullException(nameof(appUser));

        appUser.Email = EmailAddress;
        appUser.PhoneNumber = PhoneNumber;
    }

    public void MapToContactDetailsEntity(ContactDetailsModel contactDetails)
    {
        contactDetails.ApplicationUserId = Id;
        contactDetails.CompanyName = CompanyName;
        contactDetails.EmailAddress = EmailAddress;
        contactDetails.AltEmailAddress = AltEmailAddress;
        contactDetails.PhoneNumber = PhoneNumber;
        contactDetails.AltPhoneNumber = AltPhoneNumber;
        contactDetails.Fax = Fax;
        contactDetails.AddressLine1 = AddressLine1;
        contactDetails.AddressLine2 = AddressLine2;
        contactDetails.City = City;
        contactDetails.Region = Region;
        contactDetails.PostalCode = PostalCode;
        contactDetails.CountryId = CountryId;
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
