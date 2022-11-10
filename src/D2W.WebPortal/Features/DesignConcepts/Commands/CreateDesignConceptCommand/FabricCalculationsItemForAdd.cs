using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.DesignConcepts.Commands.CreateDesignConceptCommand
{
    public class FabricCalculationsItemForAdd
    {
        #region Public Properties

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
    }
}