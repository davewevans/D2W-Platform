using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.DesignConcepts.Commands.CreateDesignConceptCommand;
using D2W.WebPortal.Features.DesignConcepts.Commands.UpdateDesignConceptCommand;
using D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConceptForEdit;
using D2W.WebPortal.Features.DesignConcepts.Queries.GetDesignConcepts;

namespace D2W.WebPortal.Interfaces.Consumers
{
    public interface IDesignConceptsClient
    {
        #region Public Methods

        Task<HttpResponseWrapper<object>> GetDesignConcept(GetDesignConceptForEditQuery request);

        Task<HttpResponseWrapper<object>> GetDesignConcepts(GetDesignConceptsQuery request);

        Task<HttpResponseWrapper<object>> CreateDesignConcept(CreateDesignConceptCommand request);

        Task<HttpResponseWrapper<object>> UpdateDesignConcept(UpdateDesignConceptCommand request);

        Task<HttpResponseWrapper<object>> DeleteDesignConcept(Guid id);

        #endregion Public Methods
    }
}