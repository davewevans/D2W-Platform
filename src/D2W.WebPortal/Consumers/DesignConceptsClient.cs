using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.DesignConcepts.Commands.CreateDesignConceptCommand;
using D2W.WebPortal.Features.DesignConcepts.Commands.UpdateDesignConceptCommand;
using D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConceptForEdit;
using D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConcepts;

namespace D2W.WebPortal.Consumers
{
    public class DesignConceptsClient : IDesignConceptsClient
    {
        #region Private Fields

        private readonly IHttpService _httpService;

        #endregion Private Fields

        #region Public Constructors

        public DesignConceptsClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<HttpResponseWrapper<object>> GetDesignConcept(GetDesignConceptForEditQuery request)
        {
            return await _httpService.Post<GetDesignConceptForEditQuery, DesignConceptForEdit>("designConcepts/GetDesignConcept", request);
        }

        public async Task<HttpResponseWrapper<object>> GetDesignConcepts(GetDesignConceptsQuery request)
        {
            return await _httpService.Post<GetDesignConceptsQuery, DesignConceptsResponse>("designConcepts/GetDesignConcepts", request);
        }

        public async Task<HttpResponseWrapper<object>> CreateDesignConcept(CreateDesignConceptCommand request)
        {
            return await _httpService.Post<CreateDesignConceptCommand, CreateDesignConceptResponse>("designConcepts/CreateDesignConcept", request);
        }

        public Task<HttpResponseWrapper<object>> UpdateDesignConcept(UpdateDesignConceptCommand request)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseWrapper<object>> DeleteDesignConcept(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods

    }
}