using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("WorkOrderItems")]
public class WorkOrderItemModel : IAuditable, IMustHaveTenant
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Item { get; set; }

    public string Description { get; set; }

    public string Color { get; set; }

    public string Fabric { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    [ForeignKey(nameof(WorkOrderModel))]
    public Guid? WorkOrderId { get; set; }

    public WorkOrderModel WorkOrder { get; set; }
}
