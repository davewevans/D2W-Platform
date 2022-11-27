using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class DraperyCalculationsForAdd
{
    #region Public Properties

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

    public DraperyCalculationsModel MapToEntity()
    {
        return new DraperyCalculationsModel
        {
            IsRepeating = IsRepeating,
            MeasurementSystem = MeasurementSystem,
            FinishedLength = FinishedLength,
            TrimOff = TrimOff,
            Hems = Hems,
            Headings = Headings,
            Puddling = Puddling,
            PatternRepeatLength = PatternRepeatLength,
            Fullness = Fullness,
            FabricWidth = FabricWidth,
            RodFaceWidth = RodFaceWidth,
            Overhang = Overhang,
            Overlap = Overlap,
            Return = Return,
        };
    }
}
