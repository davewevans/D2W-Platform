using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Clients.Commands.CreateClient
{
    public class CreateClientResponse
    {
        #region Public Properties

        public Guid Id { get; set; }
        public string SuccessMessage { get; set; }

        #endregion Public Properties
    }
}