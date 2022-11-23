using D2W.Application.Common.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Commands.UpdateFabric;

public class UpdateFabricCommand : IRequest<Envelope<string>>
{
    #region Public Properties
    public Guid Id { get; set; }
    public string ManufacturerName { get; set; }
    public string BrandName { get; set; }
    public string MaterialType { get; set; }
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


    public void MapToEntity(FabricModel fabric)
    {
        if (fabric == null)
            throw new ArgumentNullException(nameof(fabric));

        fabric.ManufacturerName = ManufacturerName;
        fabric.BrandName = BrandName;
        fabric.MaterialType = MaterialType;
        fabric.ProductNumber = ProductNumber;
        fabric.Pattern = Pattern;
        fabric.Color = Color;
        fabric.SwatchImageUri = SwatchImageUri;
        fabric.CostPerYard = CostPerYard;
        fabric.CostPerMeter = CostPerMeter;
        fabric.IsRepeating = IsRepeating;
        fabric.VerticalRepeatInInches = VerticalRepeatInInches;
        fabric.VerticalRepeatInCentimeters = VerticalRepeatInCentimeters;
        fabric.HorizontalRepeatInInches = HorizontalRepeatInInches;
        fabric.HorizontalRepeatInCentimeters = HorizontalRepeatInCentimeters;
        fabric.WidthInInches = WidthInInches;
        fabric.WidthInCentimeters = WidthInCentimeters;
    }
    

    #endregion Public Methods

    #region Public Classes

    public class UpdateFabricCommandHandler : IRequestHandler<UpdateFabricCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IFabricUseCase _fabricUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateFabricCommandHandler(IFabricUseCase fabricUseCase)
        {
            _fabricUseCase = fabricUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(UpdateFabricCommand request, CancellationToken cancellationToken)
        {
            return await _fabricUseCase.EditFabric(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
