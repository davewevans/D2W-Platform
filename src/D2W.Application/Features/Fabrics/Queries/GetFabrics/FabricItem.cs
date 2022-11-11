using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Queries.GetFabrics;

public class FabricItem : AuditableDto
{

    #region Public Properties

    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string ManufacturerName { get; set; }
    public string BrandName { get; set; }
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

    #endregion Public Properties

    #region Public Methods

    public static FabricItem MapFromEntity(FabricModel fabric)
    {
        return new()
        {
            Id = fabric.Id,
            TenantId = fabric.TenantId,
            ManufacturerName = fabric.ManufacturerName,
            BrandName = fabric.BrandName,
            ProductNumber = fabric.ProductNumber,
            Pattern = fabric.Pattern,
            Color = fabric.Color,
            SwatchImageUri = fabric.SwatchImageUri,
            CostPerYard = fabric.CostPerYard,
            CostPerMeter = fabric.CostPerMeter,
            IsRepeating = fabric.IsRepeating,
            VerticalRepeatInInches = fabric.VerticalRepeatInInches,
            VerticalRepeatInCentimeters = fabric.VerticalRepeatInCentimeters,
            HorizontalRepeatInInches = fabric.HorizontalRepeatInInches,
            HorizontalRepeatInCentimeters = fabric.HorizontalRepeatInCentimeters,
            WidthInInches = fabric.WidthInInches,
            WidthInCentimeters = fabric.WidthInCentimeters,

            CreatedOn = fabric.CreatedOn,
            CreatedBy = fabric.CreatedBy,
            ModifiedOn = fabric.ModifiedOn,
            ModifiedBy = fabric.ModifiedBy,
        };
    }

    #endregion Public Methods
}
