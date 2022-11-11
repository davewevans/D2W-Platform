using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Fabrics.Queries.GetFabrics;

namespace D2W.WebPortal.Consumers
{
    public class FabricsClient : IFabricsClient
    {
        #region Private Fields

        private readonly IHttpService _httpService;

        #endregion Private Fields

        #region Public Constructors

        public FabricsClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion Public Constructors

         public async Task<HttpResponseWrapper<object>> GetFabrics(GetFabricsQuery request)
        {
            return await _httpService.Post<GetFabricsQuery, FabricsResponse>("fabrics/GetFabrics", request);
        }
    }
}