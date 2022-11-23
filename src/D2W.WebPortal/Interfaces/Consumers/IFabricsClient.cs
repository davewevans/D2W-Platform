using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Fabrics.Commands.CreateFabric;
using D2W.WebPortal.Features.Fabrics.Commands.UpdateFabric;
using D2W.WebPortal.Features.Fabrics.Queries.GetFabricForEdit;
using D2W.WebPortal.Features.Fabrics.Queries.GetFabrics;

namespace D2W.WebPortal.Interfaces.Consumers
{
    public interface IFabricsClient
    {
        #region Public Methods

        Task<HttpResponseWrapper<object>> GetFabric(GetFabricForEditQuery request);

        Task<HttpResponseWrapper<object>> GetFabrics(GetFabricsQuery request);

        Task<HttpResponseWrapper<object>> CreateFabric(CreateFabricCommand request);

        Task<HttpResponseWrapper<object>> UpdateFabric(UpdateFabricCommand request);

        Task<HttpResponseWrapper<object>> DeleteFabric(Guid id);

        #endregion Public Methods
    }
}