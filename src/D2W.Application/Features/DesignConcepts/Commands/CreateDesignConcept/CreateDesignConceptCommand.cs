using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;

public class CreateDesignConceptCommand : IRequest<Envelope<CreateDesignConceptResponse>>
{
    #region Public Constructors

    public CreateDesignConceptCommand()
    {
        FabricCalculationsItems = new List<FabricCalculationsItemForAdd>();
    }

    #endregion Public Constructors

    #region Public Properties

    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public string ClientId { get; set; }

    public WindowMeasurementsItemForAdd WindowMeasurementsItem { get; set; }
    public List<FabricCalculationsItemForAdd> FabricCalculationsItems { get; set; }

    #endregion Public Properties

    #region Public Methods

    public DesignConceptModel MapToEntity()
    {
        return new()
        {
            Name = Name,
            ImageUrl = ImageUrl,
            ClientId = ClientId,
            WindowMeasurements = WindowMeasurementsItemForAdd.MapToEntity(WindowMeasurementsItem),
            FabricCalculations = FabricCalculationsItems?.Select(fc => FabricCalculationsItemForAdd.MapToEntity(fc)).ToList()
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