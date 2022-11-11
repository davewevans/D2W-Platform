using D2W.Application.Common.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Commands.CreateFabric;

public class CreateFabricCommand : IRequest<Envelope<CreateFabricResponse>>
{

    #region Public Properties

    public string ManufacturerName { get; set; }
    public string BrandName { get; set; }
    public string ProductNumber { get; set; }
    public string Pattern { get; set; }
    public string Color { get; set; }
    public string SwatchImageUri { get; set; }
    public float CostPerYard { get; set; }
    public float CostPerMeter { get; set; }
    public bool IsRepeating { get; set; }
    public float VerticalRepeatInInches { get; set; }
    public float VerticalRepeatInCentimeters { get; set; }
    public float HorizontalRepeatInInches { get; set; }
    public float HorizontalRepeatInCentimeters { get; set; }
    public float WidthInInches { get; set; }
    public float WidthInCentimeters { get; set; }


    #endregion Public Properties

    #region Public Methods

    public FabricModel MapToEntity()
    {
        return new()
        {
            ManufacturerName = ManufacturerName,
            BrandName = BrandName,
            ProductNumber = ProductNumber,
            Pattern = Pattern,
            Color = Color,
            SwatchImageUri = SwatchImageUri,
            CostPerYard = CostPerYard,
            CostPerMeter = CostPerMeter,
            IsRepeating = IsRepeating,
            VerticalRepeatInInches = VerticalRepeatInInches,
            VerticalRepeatInCentimeters = VerticalRepeatInCentimeters,
            HorizontalRepeatInInches = HorizontalRepeatInInches,
            HorizontalRepeatInCentimeters = HorizontalRepeatInCentimeters,
            WidthInInches = WidthInInches,
            WidthInCentimeters = WidthInCentimeters,
        };
    }


    #endregion Public Methods

    #region Public Classes

    public class CreateFabricCommandHandler : IRequestHandler<CreateFabricCommand, Envelope<CreateFabricResponse>>
    {
        #region Private Fields

        private readonly IFabricUseCase _fabricUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CreateFabricCommandHandler(IFabricUseCase fabricUseCase)
        {
            _fabricUseCase = fabricUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateFabricResponse>> Handle(CreateFabricCommand request, CancellationToken cancellationToken)
        {
            return await _fabricUseCase.AddFabric(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
