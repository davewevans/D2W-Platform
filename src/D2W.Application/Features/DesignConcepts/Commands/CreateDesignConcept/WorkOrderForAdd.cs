using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class WorkOrderForAdd
{
    #region Public Properties

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

    public ICollection<WorkOrderItemForAdd> WorkOrderItems { get; set; }


    #endregion Public Properties

    #region Public Methods

    public WorkOrderModel MapToEntity()
    {
        return new WorkOrderModel
        {
            MeasurementSystem = MeasurementSystem,
            WorkOrderNumber = WorkOrderNumber,
            OpenedByWorkroom = OpenedByWorkroom,
            SentToWorkroomAt = SentToWorkroomAt,
            OpenByWorkroomAt = OpenByWorkroomAt,
            Date = Date,
            ReadyDate = ReadyDate,
            DecoratorName = DecoratorName,
            Quantity = Quantity,
            HasBoards = HasBoards,
            HasPole = HasPole,
            HasBlocks = HasBlocks,
            IsLined = IsLined,
            NumberOfWidths = NumberOfWidths,
            RodFaceWidth = RodFaceWidth,
            Overhang = Overhang,
            Overlap = Overlap,
            Return = Return,
            Hem = Hem,
            HeaderTopBottom = HeaderTopBottom,
            FinishedLength = FinishedLength,
            CordingSize = CordingSize,
            SpecialInstructions = SpecialInstructions,

            WorkOrderItems = WorkOrderItems.Select(x => x.MapToEntity()).ToList(),
        };
    }

    #endregion Public Methods
}
