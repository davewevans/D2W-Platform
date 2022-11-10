using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Clients.Queries.GetClients;

namespace D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConcepts
{
    public class DesignConceptItem : AuditableDto
    {
        #region Public Constructors

        public DesignConceptItem()
        {
            FabricCalculationsItems = new List<FabricCalculationsItem>();
        }

        #endregion Public Constructors


        #region Public Properties

        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public Guid? ClientId { get; set; }

        public ClientItem Client { get; set; }

        public bool IsArchived { get; set; }

        public bool ApprovedByClient { get; set; }

        public string ClientNotes { get; set; }

        public WindowMeasurementsItem WindowMeasurementsItem { get; set; }
        public List<FabricCalculationsItem> FabricCalculationsItems { get; set; }

        #endregion Public Properties
    }
}