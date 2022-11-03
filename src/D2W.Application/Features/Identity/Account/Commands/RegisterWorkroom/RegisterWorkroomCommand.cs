using D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;

public class RegisterWorkroomCommand : IRequest<Envelope<RegisterWorkroomResponse>>
{
    #region Public Properties

    public string Email { get; set; }
    public string PhoneNumber { get; set; }


    public string CompanyName { get; set; }

    [Phone]
    public string AltPhone { get; set; }

    [EmailAddress]
    public string AltEmailAddress { get; set; }

    public string ContactName1 { get; set; }
    public string ContactName2 { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public Guid CountryId { get; set; }
    public string LogoUri { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity()
    {
        return new()
        {
            UserName = Email,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Name = CompanyName,
            AppUserType = ApplicationUserType.Workroom,
            ContactDetails = new ContactDetailsModel
            {
                CompanyName = CompanyName,
                AltPhone1 = AltPhone,
                AltEmailAddress1 = AltEmailAddress,
                ContactName1 = ContactName1,
                ContactName2 = ContactName2,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                City = City,
                Region = Region,
                PostalCode = PostalCode,
                LogoUri = LogoUri,
                CountryId = CountryId
            }
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
