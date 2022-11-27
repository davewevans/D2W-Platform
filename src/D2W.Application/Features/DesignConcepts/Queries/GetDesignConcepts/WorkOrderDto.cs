using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class WorkOrderDto : AuditableDto
{
    #region Public Properties

    public Guid Id { get; set; }
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

    public Guid DesignConceptId { get; set; }

    public ICollection<WorkOrderItemDto> WorkOrderItems { get; set; }
    public DesignConceptDto DesignConcept { get; set; }


    #endregion Public Properties

    #region Public Methods

    public static WorkOrderDto MapFromEntity(WorkOrderModel workOrder)
    {
        return new WorkOrderDto
        {
            Id = workOrder.Id,
            MeasurementSystem = workOrder.MeasurementSystem,
            WorkOrderNumber = workOrder.WorkOrderNumber,
            OpenedByWorkroom = workOrder.OpenedByWorkroom,
            SentToWorkroomAt = workOrder.SentToWorkroomAt,
            OpenByWorkroomAt = workOrder.OpenByWorkroomAt,
            Date = workOrder.Date,
            ReadyDate = workOrder.ReadyDate,
            DecoratorName = workOrder.DecoratorName,
            Quantity = workOrder.Quantity,
            HasBoards = workOrder.HasBoards,
            HasPole =workOrder.HasPole,
            HasBlocks = workOrder.HasBlocks,
            IsLined = workOrder.IsLined,
            NumberOfWidths = workOrder.NumberOfWidths,
            RodFaceWidth = workOrder.RodFaceWidth,
            Overhang = workOrder.Overhang,
            Overlap = workOrder.Overlap,
            Return = workOrder.Return,
            Hem = workOrder.Hem,
            HeaderTopBottom = workOrder.HeaderTopBottom,
            FinishedLength = workOrder.FinishedLength,
            CordingSize = workOrder.CordingSize,
            SpecialInstructions = workOrder.SpecialInstructions,

            WorkOrderItems = workOrder.WorkOrderItems.Select(WorkOrderItemDto.MapFromEntity).ToList(),

            CreatedOn = workOrder.CreatedOn,
            CreatedBy = workOrder.CreatedBy,
            ModifiedOn = workOrder.ModifiedOn,
            ModifiedBy = workOrder.ModifiedBy,
        };
    }

    #endregion Public Methods
}
