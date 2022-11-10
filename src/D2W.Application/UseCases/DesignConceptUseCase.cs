using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Common.Managers;
using D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConceptForEdit;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

namespace D2W.Application.UseCases;

public class DesignConceptUseCase : IDesignConceptUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly ApplicationUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportingService _reportingService;

    #endregion Private Fields

    #region Public Constructors

    public DesignConceptUseCase(IApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IReportingService reportingService, ApplicationUserManager userManager)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _reportingService = reportingService;
        _userManager = userManager;
    }

    #endregion Public Constructors

    public async Task<Envelope<DesignConceptForEdit>> GetDesignConcept(GetDesignConceptForEditQuery request)
    {
        throw new NotImplementedException();
    }

    public async Task<Envelope<DesignConceptsResponse>> GetDesignConcepts(GetDesignConceptsQuery request)
    {
        var query = _dbContext.DesignConcepts
            .Include(x => x.WindowMeasurements)
            .Include(x => x.FabricCalculations)
            .Include(x => x.ApplicationUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.Name.Contains(request.SearchText) 
                                     || x.ApplicationUser.FullName.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(x => x.Name);

        var designConcepts = await query
            .Select(designConcept => DesignConceptItem.MapFromEntity(designConcept))
            .AsNoTracking()
            .ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var designConceptsResponse = new DesignConceptsResponse
        {
            DesignConcepts = designConcepts
        };

        return Envelope<DesignConceptsResponse>.Result.Ok(designConceptsResponse);
    }

    public async Task<Envelope<CreateDesignConceptResponse>> AddDesignConcept(CreateDesignConceptCommand request)
    {
        var designConcept = request.MapToEntity();

        await _dbContext.DesignConcepts.AddAsync(designConcept);

        await _dbContext.SaveChangesAsync();

        var createDesignConceptResponse = new CreateDesignConceptResponse
        {
            Id = designConcept.Id.ToString(),
            SuccessMessage = Resource.Applicant_has_been_created_successfully
        };

        return Envelope<CreateDesignConceptResponse>.Result.Ok(createDesignConceptResponse);
    }
}
