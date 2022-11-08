using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.RegisterClient;

public class RegisterClientCommand : IRequest<Envelope<RegisterClientResponse>>
{
    #region Public Properties

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AvatarUri { get; set; }

    public string ContactName2 { get; set; }
    [Phone]
    public string AltPhone1 { get; set; }

    [EmailAddress]
    public string AltEmailAddress { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity()
    {
        var (firstName, lastName) = SplitFullName();

        return new()
        {
            UserName = Email,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Name = firstName,
            Surname = lastName,
            AppUserType = ApplicationUserType.Client,
            AvatarUri = AvatarUri
        };
    }

    public ContactDetailsModel MapToContactDetailsEntity()
    {
        return new ContactDetailsModel()
        {
            ContactName1 = FullName?.Trim(),
            ContactName2 = ContactName2?.Trim(),
            AltPhone1 = AltPhone1?.Trim(),
            AltEmailAddress1 = AltEmailAddress?.Trim(),

        };
    }

    #endregion Public Methods

    #region methods

    private (string, string) SplitFullName()
    {
        var nameSplit = FullName?.Split(' ');
        string firstName = string.Empty;
        string lastName = string.Empty;
        if (nameSplit != null)
        {
            firstName = nameSplit[0];
            lastName = nameSplit[^1];
        }

        return (firstName, lastName);
    }

    #endregion



    #region Public Classes

    public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, Envelope<RegisterClientResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public RegisterClientCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<RegisterClientResponse>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.RegisterClient(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
