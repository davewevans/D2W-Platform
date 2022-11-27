using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class WorkOrderItemDto : AuditableDto
{
    #region Public Properties

    public Guid Id { get; set; }

    public WorkOrderItemType WorkOrderItemType { get; set; }

    public MeasurementSystem MeasurementSystem { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public string Description { get; set; }

    public Guid? FabricId { get; set; }

    public float Yardage { get; set; }

    public float Meters { get; set; }

    public FabricWorkOrderItemDto Fabric { get; set; }


    #endregion Public Properties


    #region Public Methods

    public static WorkOrderItemDto MapFromEntity(WorkOrderItemModel workOrderItem)
    {
        return new WorkOrderItemDto
        {
            Id = workOrderItem.Id,
            MeasurementSystem = workOrderItem.MeasurementSystem,
            WorkOrderItemType = workOrderItem.WorkOrderItemType,
            Name = workOrderItem.Name,
            Value = workOrderItem.Value,
            Description = workOrderItem.Description,
            FabricId = workOrderItem.FabricId,
            Yardage = workOrderItem.Yardage,
            Meters = workOrderItem.Meters,

            CreatedOn = workOrderItem.CreatedOn,
            CreatedBy = workOrderItem.CreatedBy,
            ModifiedOn = workOrderItem.ModifiedOn,
            ModifiedBy = workOrderItem.ModifiedBy,
        };
    }

    #endregion Public Methods
}
