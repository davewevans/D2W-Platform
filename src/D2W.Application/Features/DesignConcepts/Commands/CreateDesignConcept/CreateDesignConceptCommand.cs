using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class CreateDesignConceptCommand : IRequest<Envelope<CreateDesignConceptResponse>>
{
    #region Public Constructors

    public CreateDesignConceptCommand()
    {
        WindowMeasurements = new WindowMeasurementsForAdd();
        DraperyCalculations = new DraperyCalculationsForAdd();
        WorkOrder = new WorkOrderForAdd();
    }

    #endregion Public Constructors

    #region Public Properties

    public string Name { get; set; }

    public string ImageUri { get; set; }

    public string ClientId { get; set; }

    public bool OpenedByClient { get; set; }

    public DateTime? SentToClientAt { get; set; }

    public DateTime? OpenByClientAt { get; set; }

    public WindowMeasurementsForAdd WindowMeasurements { get; set; }
    public DraperyCalculationsForAdd DraperyCalculations { get; set; }
    public WorkOrderForAdd WorkOrder { get; set; }

    #endregion Public Properties

    #region Public Methods

    public DesignConceptModel MapToEntity()
    {
        return new()
        {
            Name = Name,
            ImageUri = ImageUri,
            ClientId = ClientId,
            WindowMeasurements = WindowMeasurements.MapToEntity(),
            DraperyCalculations = DraperyCalculations.MapToEntity(),
            WorkOrder = WorkOrder.MapToEntity()
        };
    }


    #endregion Public Methods

    #region Public Classes

    public class CreateDesignConceptCommandHandler : IRequestHandler<CreateDesignConceptCommand, Envelope<CreateDesignConceptResponse>>
    {
        #region Private Fields
            
        private readonly IDesignConceptUseCase _designConceptUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CreateDesignConceptCommandHandler(IDesignConceptUseCase designConceptUseCase)
        {
            _designConceptUseCase = designConceptUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateDesignConceptResponse>> Handle(CreateDesignConceptCommand request, CancellationToken cancellationToken)
        {
            return await _designConceptUseCase.AddDesignConcept(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}