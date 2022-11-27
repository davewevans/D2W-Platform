using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class FabricWorkOrderItemDto
{
    public Guid Id { get; set; }
    public string ManufacturerName { get; set; }
    public string BrandName { get; set; }
    public string MaterialType { get; set; }
    public string ProductNumber { get; set; }
    public string Pattern { get; set; }
    public string Color { get; set; }
    public string SwatchImageUri { get; set; }

    public static FabricWorkOrderItemDto MapFromEntity(FabricModel fabric)
    {
        return new FabricWorkOrderItemDto
        {
            Id = fabric.Id,
            ManufacturerName = fabric.ManufacturerName,
            BrandName = fabric.BrandName,
            MaterialType = fabric.MaterialType,
            ProductNumber = fabric.ProductNumber,
            Pattern = fabric.Pattern,
            Color = fabric.Color,
            SwatchImageUri = fabric.SwatchImageUri
        };
    }
}
