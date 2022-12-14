using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Domain.Entities.Identity;

namespace D2W.Domain.Entities;

[Table("DesignConcepts")]
public class DesignConceptModel : IAuditable, IMustHaveTenant
{
    public DesignConceptModel()
    {
        WindowMeasurements = new WindowMeasurementsModel();
        DraperyCalculations = new DraperyCalculationsModel();
        WorkOrder = new WorkOrderModel();
    }

    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string ClientId { get; set; }

    public bool OpenedByClient { get; set; }
    public DateTime? SentToClientAt { get; set; }
    public DateTime? OpenByClientAt { get; set; }

    public string Name { get; set; }

    public string ImageUri { get; set; }

    public bool ApprovedByClient { get; set; }

    public bool IsArchived { get; set; }

    public string ClientNotes { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }


    #region Navigational Properties

    // One-to-many for Client app user
    public ApplicationUser Client { get; set; }
    public WindowMeasurementsModel WindowMeasurements { get; set; }
    public DraperyCalculationsModel DraperyCalculations { get; set; }
    public WorkOrderModel WorkOrder { get; set; }

    #endregion Navigational Properties
}
