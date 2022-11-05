using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Clients.Queries.GetClients;

public class ClientsResponse
{
    #region Public Properties

    public PagedList<ClientItem> Clients { get; set; }

    #endregion Public Properties
}
