namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class WindowMeasurementsItemForAdd
{
    #region Public Properties

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

    public static WindowMeasurementsModel MapToEntity(WindowMeasurementsItemForAdd windowMeasurements)
    {
        return new WindowMeasurementsModel
        {
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
        };
    }
}