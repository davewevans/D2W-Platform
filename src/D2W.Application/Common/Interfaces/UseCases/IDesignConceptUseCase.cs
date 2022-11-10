using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConceptForEdit;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

namespace D2W.Application.Common.Interfaces.UseCases;

public interface IDesignConceptUseCase
{
    #region Public Methods

    Task<Envelope<DesignConceptForEdit>> GetDesignConcept(GetDesignConceptForEditQuery request);
    Task<Envelope<DesignConceptsResponse>> GetDesignConcepts(GetDesignConceptsQuery request);
    Task<Envelope<CreateDesignConceptResponse>> AddDesignConcept(CreateDesignConceptCommand request);

    //Task<Envelope<string>> EditDesignConcept(UpdateDesignConceptCommand request);
    //Task<Envelope<string>> DeleteDesignConcept(DeleteDesignConceptCommand request);


    #endregion Public Methods
}
