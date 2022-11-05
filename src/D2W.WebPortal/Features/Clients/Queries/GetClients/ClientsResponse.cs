using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Clients.Queries.GetClients
{
    public class ClientsResponse
    {
        #region Public Properties

        public PagedList<ClientItem> Clients { get; set; }

        #endregion Public Properties
    }
}