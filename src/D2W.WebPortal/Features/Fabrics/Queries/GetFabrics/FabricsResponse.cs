using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Fabrics.Queries.GetFabrics
{
    public class FabricsResponse
    {
        #region Public Properties

        public PagedList<FabricItem> Fabrics { get; set; }

        #endregion Public Properties
    }
}