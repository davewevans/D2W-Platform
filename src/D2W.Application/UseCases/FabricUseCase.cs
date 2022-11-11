using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Common.Managers;
using D2W.Application.Features.Fabrics.Commands.CreateFabric;
using D2W.Application.Features.Fabrics.Queries.GetFabrics;
using Microsoft.EntityFrameworkCore;

namespace D2W.Application.UseCases;

public class FabricUseCase : IFabricUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportingService _reportingService;

    #endregion Private Fields

    #region Public Constructors

    public FabricUseCase(IApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IReportingService reportingService)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _reportingService = reportingService;
    }

    #endregion Public Constructors

    public async Task<Envelope<FabricsResponse>> GetFabrics(GetFabricsQuery request)
    {
        var query = _dbContext.Fabrics.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.Pattern.Contains(request.SearchText) 
                                     || x.Color.Contains(request.SearchText) 
                                     || x.BrandName.Contains(request.SearchText) 
                                     || x.ManufacturerName.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(x => x.Pattern);

        var fabrics = await query
            .Select(fabric => FabricItem.MapFromEntity(fabric))
            .AsNoTracking()
            .ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var fabricResponse = new FabricsResponse()
        {
            Fabrics = fabrics
        };

        return Envelope<FabricsResponse>.Result.Ok(fabricResponse);
    }
    public async Task<Envelope<CreateFabricResponse>> AddFabric(CreateFabricCommand request)
    {
        var fabric = request.MapToEntity();

        await _dbContext.Fabrics.AddAsync(fabric);

        await _dbContext.SaveChangesAsync();

        var createFabricResponse = new CreateFabricResponse()
        {
            Id = fabric.Id.ToString(),
            SuccessMessage = Resource.Fabric_has_been_created_successfully
        };

        return Envelope<CreateFabricResponse>.Result.Ok(createFabricResponse);
    }

}
