using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("DraperyCalculations")]
public class DraperyCalculationsModel : IAuditable, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public MeasurementSystem MeasurementSystem { get; set; }
    public bool IsRepeating { get; set; }
    public float FinishedLength { get; set; }
    public float TrimOff { get; set; }
    public float Hems { get; set; }
    public float Headings { get; set; }
    public float Puddling { get; set; }
    public float PatternRepeatLength { get; set; }
    public float Fullness { get; set; }
    public float FabricWidth { get; set; }
    public float RodFaceWidth { get; set; }
    public float Overhang { get; set; }
    public float Overlap { get; set; }
    public float Return { get; set; }

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
