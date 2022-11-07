using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;


[Table("WindowMeasurements")]
public class WindowMeasurementsModel : IAuditable, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public MeasurementSystem MeasurementSystem { get; set; }
    public string Notes { get; set; }
    public string Room { get; set; }
    public string Window { get; set; }
    public float OutsideLeftToRight { get; set; } // A
    public float OutsideTopToBottom { get; set; } // B
    public float InsideLeftToRight { get; set; } // C
    public float InsideTopToBottom { get; set; } // D
    public float TopFrameToCeilingOrCrown { get; set; } // E
    public float BottomFrameToFloor { get; set; } // F
    public float FloorToCeilingOrCrown { get; set; } // G
    public float TopFrameToFloor { get; set; } // H
    public float LeftCasingToWallOrObstruction { get; set; } // I
    public float RightCasingToWallOrObstruction { get; set; } // J
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    public Guid DesignConceptId { get; set; }

    #region Navigational Properties
    public DesignConceptModel DesignConcept { get; set; }

    #endregion Navigational Properties

}
