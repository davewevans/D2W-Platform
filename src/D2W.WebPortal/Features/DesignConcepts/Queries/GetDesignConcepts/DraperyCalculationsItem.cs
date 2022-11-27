using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.DesignConcepts.Commands.Shared;

namespace D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConcepts
{
    public class DraperyCalculationsItem : DraperyCalculationsBase
    {
        #region Public Properties

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        #endregion Public Properties
    }
}