using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class WindowMeasurementsItem : AuditableDto
{
    #region Public Properties

    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public MeasurementSystem MeasurementSystem { get; set; }
    public string Notes { get; set; }
    public string Room { get; set; }
    public string Window { get; set; }
    public float OutsideLeftToRight { get; set; } // A
    public float OutsideTopToBottom { get; set; } // B
    public float InsideLeftToRight { get; set; } // C
    public float InsideTopToBottom { get; set; } // D
    public float TopFrameToCeilingOrCrown { get; set; } // E
    public float BottomFrameToFloor { get; set; } // F
    public float FloorToCeilingOrCrown { get; set; } // G
    public float TopFrameToFloor { get; set; } // H
    public float LeftCasingToWallOrObstruction { get; set; } // I
    public float RightCasingToWallOrObstruction { get; set; } // J

    #endregion Public Properties


    #region Public Methods

    public static WindowMeasurementsItem MapFromEntity(WindowMeasurementsModel windowMeasurements)
    {
        return new WindowMeasurementsItem
        {
            Id = windowMeasurements.Id,
            TenantId = windowMeasurements.TenantId,
            MeasurementSystem = windowMeasurements.MeasurementSystem,
            Notes = windowMeasurements.Notes,
            Room = windowMeasurements.Room,
            Window = windowMeasurements.Room,
            OutsideLeftToRight = windowMeasurements.OutsideLeftToRight,
            OutsideTopToBottom = windowMeasurements.OutsideTopToBottom,
            InsideLeftToRight = windowMeasurements.InsideLeftToRight,
            InsideTopToBottom = windowMeasurements.InsideTopToBottom,
            TopFrameToCeilingOrCrown = windowMeasurements.TopFrameToCeilingOrCrown,
            BottomFrameToFloor = windowMeasurements.BottomFrameToFloor,
            FloorToCeilingOrCrown = windowMeasurements.FloorToCeilingOrCrown,
            TopFrameToFloor = windowMeasurements.TopFrameToFloor,
            LeftCasingToWallOrObstruction = windowMeasurements.LeftCasingToWallOrObstruction,
            RightCasingToWallOrObstruction = windowMeasurements.RightCasingToWallOrObstruction,

            CreatedOn = windowMeasurements.CreatedOn,
            CreatedBy = windowMeasurements.CreatedBy,
            ModifiedOn = windowMeasurements.ModifiedOn,
            ModifiedBy = windowMeasurements.ModifiedBy
        };
    }

    #endregion Public Methods
}
