﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Features.Workrooms.Commands.DeleteWorkroom;
using D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;
using D2W.Application.Features.Workrooms.Queries.GetWorkrooms;
using D2W.Application.Features.Workrooms.Commands.UpdateWorkroom;
using D2W.Application.Common.Managers;

namespace D2W.Application.UseCases;

public class WorkroomUseCase : IWorkroomUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly ApplicationUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportingService _reportingService;
    private readonly ITenantResolver _tenenResolver;

    #endregion Private Fields

    #region Public Constructors

    public WorkroomUseCase(IApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IReportingService reportingService, ApplicationUserManager userManager, ITenantResolver tenenResolver)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _reportingService = reportingService;
        _userManager = userManager;
        _tenenResolver = tenenResolver;
    }

    #endregion Public Constructors

    #region Public Methods


    public async Task<Envelope<WorkroomForEdit>> GetWorkroom(GetWorkroomForEditQuery request)
    {
        var workroom = await _userManager.FindByIdAsync(request.Id);

        if (workroom == null)
            return Envelope<WorkroomForEdit>.Result.NotFound(Resource.Unable_to_load_Workroom);

        var workroomForEdit = WorkroomForEdit.MapFromEntity(workroom);

        return Envelope<WorkroomForEdit>.Result.Ok(workroomForEdit);
    }

    public async Task<Envelope<WorkroomsResponse>> GetWorkrooms(GetWorkroomsQuery request)
    {
        var query = _dbContext.TenantsWorkrooms.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(tc => tc.Workroom.Name.Contains(request.SearchText) || tc.Workroom.Surname.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(tc => tc.Workroom.Name).ThenBy(tc => tc.Workroom.Surname);

        var workroomItems = await query
            .Include(q => q.Workroom.ContactDetails)
            .Select(q => WorkroomItem.MapFromEntity(q.Workroom, q.TenantId))
            .AsNoTracking()
            .ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var workroomsResponse = new WorkroomsResponse
        {
            Workrooms = workroomItems
        };

        return Envelope<WorkroomsResponse>.Result.Ok(workroomsResponse);
    }

    public async Task<Envelope<string>> EditWorkroom(UpdateWorkroomCommand request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_ApplicationUser_Id);

        var workroom = await _userManager.FindByIdAsync(request.Id);

        if (workroom == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_Workroom);

        request.MapToEntity(workroom);

        _dbContext.Users.Update(workroom);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Workroom_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteWorkroom(DeleteWorkroomCommand request)
    {
        // Workroom only deleted in many-to-many as related to tenant

        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_Workroom_Id);

        var tenantId = _tenenResolver.GetTenantId();

        if (!tenantId.HasValue)
            return Envelope<string>.Result.NotFound(Resource.Tenant_not_found);

        var tenantWorkroom = await _dbContext.TenantsWorkrooms.FirstOrDefaultAsync(x =>
            x.TenantId.Equals(tenantId.Value) && x.ApplicationUserId.Equals(request.Id));

        if (tenantWorkroom == null)
            return Envelope<string>.Result.NotFound(Resource.The_Workroom_is_not_found);

        _dbContext.TenantsWorkrooms.Remove(tenantWorkroom);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Workroom_has_been_deleted_successfully);
    }

    #endregion Public Methods

}
