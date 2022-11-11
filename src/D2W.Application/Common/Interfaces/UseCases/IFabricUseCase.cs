using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Features.Fabrics.Commands.CreateFabric;
using D2W.Application.Features.Fabrics.Queries.GetFabrics;

namespace D2W.Application.Common.Interfaces.UseCases;

public interface IFabricUseCase
{
    #region Public Methods

    //Task<Envelope<FabricForEdit>> GetFabric(GetFabricForEditQuery request);
    Task<Envelope<FabricsResponse>> GetFabrics(GetFabricsQuery request);
    Task<Envelope<CreateFabricResponse>> AddFabric(CreateFabricCommand request);

    //Task<Envelope<string>> EditFabric(UpdateFabricCommand request);
    //Task<Envelope<string>> DeleteFabric(DeleteFabricCommand request);


    #endregion Public Methods
}
