using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Fabrics.Commands.CreateFabric
{
    public class CreateFabricCommand
    {
        public string ManufacturerName { get; set; }
        public string BrandName { get; set; }
        public string MaterialType { get; set; }
        public string ProductNumber { get; set; }
        public string Pattern { get; set; }
        public string Color { get; set; }
        public string SwatchImageUri { get; set; }
        public float CostPerYard { get; set; }
        public float CostPerMeter { get; set; }
        public bool IsRepeating { get; set; }
        public float VerticalRepeatInInches { get; set; }
        public float VerticalRepeatInCentimeters { get; set; }
        public float HorizontalRepeatInInches { get; set; }
        public float HorizontalRepeatInCentimeters { get; set; }
        public float WidthInInches { get; set; }
        public float WidthInCentimeters { get; set; }
    }
}