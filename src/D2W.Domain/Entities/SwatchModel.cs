using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("Swatches")]
public class SwatchModel : IAuditable, IMustHaveTenant
{
    // TODO relationship to workroom

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUri { get; set; }
    public Guid TenantId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}
