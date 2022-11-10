using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class FabricCalculationsItem : AuditableDto
{
    #region Public Properties

    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public MeasurementSystem MeasurementSystem { get; set; }
    public FabricPriority FabricPriority { get; set; }
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

    public static FabricCalculationsItem MapFromEntity(FabricCalculationsModel fabricCalculations)
    {
        return new FabricCalculationsItem
        {
            Id = fabricCalculations.Id,
            TenantId = fabricCalculations.TenantId,
            MeasurementSystem = fabricCalculations.MeasurementSystem,
            FabricPriority = fabricCalculations.FabricPriority,
            FinishedLength = fabricCalculations.FinishedLength,
            TrimOff = fabricCalculations.TrimOff,
            Hems = fabricCalculations.Hems,
            Headings = fabricCalculations.Headings,
            Puddling = fabricCalculations.Puddling,
            PatternRepeatLength = fabricCalculations.PatternRepeatLength,
            Fullness = fabricCalculations.Fullness,
            FabricWidth = fabricCalculations.FabricWidth,
            RodFaceWidth = fabricCalculations.RodFaceWidth,
            Overhang = fabricCalculations.Overhang,
            Overlap = fabricCalculations.Overlap,
            Return = fabricCalculations.Return,
            CreatedOn = fabricCalculations.CreatedOn,
            CreatedBy = fabricCalculations.CreatedBy,
            ModifiedOn = fabricCalculations.ModifiedOn,
            ModifiedBy = fabricCalculations.ModifiedBy,
        };
    }

    #endregion Public Methods
}
