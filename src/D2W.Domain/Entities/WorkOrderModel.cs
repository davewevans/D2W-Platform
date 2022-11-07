using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("WorkOrders")]
public class WorkOrderModel : IAuditable, IMustHaveTenant
{
    public WorkOrderModel()
    {
        //WorkOrderItems = new List<WorkOrderItemModel>();
    }

    public Guid Id { get; set; }

    public int WorkOrderNumber { get; set; }

    public Guid TenantId { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }


    #region Navigational Properties

    // TODO may not need work order items

    // public ICollection<WorkOrderItemModel> WorkOrderItems { get; set; }

    #endregion Navigational Properties

}
