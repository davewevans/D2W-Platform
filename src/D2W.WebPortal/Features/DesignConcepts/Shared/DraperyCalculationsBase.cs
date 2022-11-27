using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.DesignConcepts.Commands.Shared
{
    public abstract class DraperyCalculationsBase : AuditableDto
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

        public int NumberOfWidths
        {
            get
            {
                return 0;
            }
            private set { }
        }

        public int TotalYardsOfFabricNeeded
        {
            get
            {
                if (IsRepeating) { }
                return 0;
            }
            private set { }
        }

        public int TotalYardsOfFabricForCascade
        {
            get
            {
                if (IsRepeating) { }
                return 0;
            }
            private set { }
        }

        #endregion Public Properties



    }
}