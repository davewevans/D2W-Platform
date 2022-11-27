using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class WorkOrderItemForAdd
{
    #region Public Properties

    public WorkOrderItemType WorkOrderItemType { get; set; }

    public MeasurementSystem MeasurementSystem { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public string Description { get; set; }

    public Guid? FabricId { get; set; }

    public float Yardage { get; set; }

    public float Meters { get; set; }


    #endregion Public Properties

    #region Public Methods

    public WorkOrderItemModel MapToEntity()
    {
        return new WorkOrderItemModel
        {
            MeasurementSystem = MeasurementSystem,
            WorkOrderItemType = WorkOrderItemType,
            Name = Name,
            Value = Value,
            Description = Description,
            FabricId = FabricId,
            Yardage = Yardage,
            Meters = Meters
        };
    }

    #endregion Public Methods
}
