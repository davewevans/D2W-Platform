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

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    public string AvatarUri { get; set; }

    #endregion Public Properties

    #region Public Methods

    public ApplicationUser MapToEntity()
    {
        var (firstName, lastName) = FullName.SplitFullName();

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
            FullName = FullName?.Trim(),
            PhoneNumber = PhoneNumber?.Trim(),
            EmailAddress = Email?.Trim(),
            AvatarUri = AvatarUri
        };
    }

    #endregion Public Methods



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
