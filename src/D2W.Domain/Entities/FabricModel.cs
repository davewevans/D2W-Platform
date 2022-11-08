using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("Fabrics")]
public class FabricModel : IAuditable, IMustHaveTenant
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }

    public string ManufacturerName { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Pattern { get; set; }
    public float CostPerYard { get; set; }
    public float CostPerMeter { get; set; }
    public bool IsRepeating { get; set; }
    public float RepeatingLengthInInches { get; set; }
    public float RepeatingLengthInCentimeters { get; set; }
    public string SwatchImageUri { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }



}
