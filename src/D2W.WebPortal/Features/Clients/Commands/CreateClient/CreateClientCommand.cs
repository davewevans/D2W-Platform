using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommand
    {
        #region Public Properties

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AvatarUri { get; set; }

        #endregion Public Properties
    }
}