namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class WindowMeasurementsForAdd
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

    public WindowMeasurementsModel MapToEntity()
    {
        return new WindowMeasurementsModel
        {
            MeasurementSystem = MeasurementSystem,
            Notes = Notes,
            Room = Room,
            Window = Room,
            OutsideLeftToRight = OutsideLeftToRight,
            OutsideTopToBottom = OutsideTopToBottom,
            InsideLeftToRight = InsideLeftToRight,
            InsideTopToBottom = InsideTopToBottom,
            TopFrameToCeilingOrCrown = TopFrameToCeilingOrCrown,
            BottomFrameToFloor = BottomFrameToFloor,
            FloorToCeilingOrCrown = FloorToCeilingOrCrown,
            TopFrameToFloor = TopFrameToFloor,
            LeftCasingToWallOrObstruction = LeftCasingToWallOrObstruction,
            RightCasingToWallOrObstruction = RightCasingToWallOrObstruction,
        };
    }
}