using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Common.Managers;
using D2W.Application.Features.Fabrics.Commands.CreateFabric;
using D2W.Application.Features.Fabrics.Commands.DeleteFabric;
using D2W.Application.Features.Fabrics.Commands.UpdateFabric;
using D2W.Application.Features.Fabrics.Queries.GetFabricForEdit;
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

    public async Task<Envelope<FabricForEdit>> GetFabric(GetFabricForEditQuery request)
    {
        var fabric = await _dbContext.Fabrics.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (fabric == null)
            return Envelope<FabricForEdit>.Result.NotFound(Resource.Unable_to_load_fabric);

        var fabricForEdit = FabricForEdit.MapFromEntity(fabric);

        return Envelope<FabricForEdit>.Result.Ok(fabricForEdit);
    }

    public async Task<Envelope<FabricsResponse>> GetFabrics(GetFabricsQuery request)
    {
        var query = _dbContext.Fabrics.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(x => x.Pattern.Contains(request.SearchText) 
                                     || x.Color.Contains(request.SearchText) 
                                     || x.BrandName.Contains(request.SearchText) 
                                     || x.ManufacturerName.Contains(request.SearchText)
                                     || x.MaterialType.Contains(request.SearchText));

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
            Id = fabric.Id,
            SuccessMessage = Resource.Fabric_has_been_created_successfully
        };

        return Envelope<CreateFabricResponse>.Result.Ok(createFabricResponse);
    }

    public async Task<Envelope<string>> EditFabric(UpdateFabricCommand request)
    {
        var fabric = await _dbContext.Fabrics.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (fabric == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_fabric);

        request.MapToEntity(fabric);

        _dbContext.Fabrics.Update(fabric);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Fabric_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteFabric(DeleteFabricCommand request)
    {
        var fabric = await _dbContext.Fabrics.FirstOrDefaultAsync(x => x.Id.Equals(request.Id));

        if (fabric == null)
            return Envelope<string>.Result.NotFound(Resource.The_fabric_is_not_found);

        _dbContext.Fabrics.Remove(fabric);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Fabric_has_been_deleted_successfully);
    }
}
