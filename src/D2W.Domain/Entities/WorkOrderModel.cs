using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Domain.Entities.Identity;

namespace D2W.Domain.Entities;

[Table("WorkOrders")]
public class WorkOrderModel : IAuditable, IMustHaveTenant
{
    public WorkOrderModel()
    {
        WorkOrderItems = new List<WorkOrderItemModel>();
    }

    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string ClientId { get; set; }
    public string WorkroomId { get; set; }

    public int WorkOrderNumber { get; set; }
    public bool OpenedByWorkroom { get; set; }
    public DateTime? SentToWorkroomAt { get; set; }
    public DateTime? OpenByWorkroomAt { get; set; }

    public DateTime Date { get; set; }
    public DateTime? ReadyDate { get; set; }
    public MeasurementSystem MeasurementSystem { get; set; }
    public string DecoratorName { get; set; }
    public int Quantity { get; set; }
    public bool HasBoards { get; set; }
    public bool HasPole { get; set; }
    public bool HasBlocks { get; set; }
    public bool IsLined { get; set; }
    public bool IsInterline { get; set; }
    public int NumberOfWidths { get; set; }
    public float RodFaceWidth { get; set; }
    public float Overhang { get; set; }
    public float Overlap { get; set; }
    public float Return { get; set; }
    public float Hem { get; set; }
    public float HeaderTopBottom { get; set; }
    public float FinishedLength { get; set; }
    public float CordingSize { get; set; }

    public string SpecialInstructions { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    public Guid DesignConceptId { get; set; }

    #region Navigational Properties

    public ICollection<WorkOrderItemModel> WorkOrderItems { get; set; }
    public DesignConceptModel DesignConcept { get; set; }

    // One-to-many for app user Workroom
    public ApplicationUser Workroom { get; set; }

    #endregion Navigational Properties

}
