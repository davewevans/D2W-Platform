using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class DraperyCalculationsDto : AuditableDto
{
    #region Public Properties

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

    #endregion Public Properties


    #region Public Methods

    public static DraperyCalculationsDto MapFromEntity(DraperyCalculationsModel draperyCalculations)
    {
        return new DraperyCalculationsDto
        {
            Id = draperyCalculations.Id,
            TenantId = draperyCalculations.TenantId,
            IsRepeating = draperyCalculations.IsRepeating,
            MeasurementSystem = draperyCalculations.MeasurementSystem,
            FinishedLength = draperyCalculations.FinishedLength,
            TrimOff = draperyCalculations.TrimOff,
            Hems = draperyCalculations.Hems,
            Headings = draperyCalculations.Headings,
            Puddling = draperyCalculations.Puddling,
            PatternRepeatLength = draperyCalculations.PatternRepeatLength,
            Fullness = draperyCalculations.Fullness,
            FabricWidth = draperyCalculations.FabricWidth,
            RodFaceWidth = draperyCalculations.RodFaceWidth,
            Overhang = draperyCalculations.Overhang,
            Overlap = draperyCalculations.Overlap,
            Return = draperyCalculations.Return,
            CreatedOn = draperyCalculations.CreatedOn,
            CreatedBy = draperyCalculations.CreatedBy,
            ModifiedOn = draperyCalculations.ModifiedOn,
            ModifiedBy = draperyCalculations.ModifiedBy,
        };
    }

    #endregion Public Methods
}
