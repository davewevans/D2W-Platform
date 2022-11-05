using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Features.Clients.Queries.GetClients;

namespace D2W.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommand : IRequest<Envelope<string>>
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

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IClientUseCase _clientUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateClientCommandHandler(IClientUseCase clientUseCase)
        {
            _clientUseCase = clientUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            return await _clientUseCase.EditClient(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
