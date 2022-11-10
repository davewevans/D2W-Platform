using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConcepts
{
    public class DesignConceptsResponse
    {
        #region Public Properties

        public PagedList<DesignConceptItem> DesignConcepts { get; set; }

        #endregion Public Properties
    }
}