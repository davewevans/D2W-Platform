using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("DesignConcepts")]
public class DesignConceptModel : IAuditable, IMustHaveTenant
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string ImageUrl { get; set; }

    public Guid ClientId { get; set; }

    public bool IsArchived { get; set; }

    public bool ApprovedByClient { get; set; }

    public string ClientNotes { get; set; }

    // diminsions
    // calculations
    
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }


    #region Navigational Properties
    public WindowMeasurementsModel WindowMeasurements { get; set; }
    public DraperyCalculationsModel DraperyCalculations { get; set; }

    #endregion Navigational Properties
}
