using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;
using D2W.Application.Common.Managers;
using D2W.Application.Features.Clients.Commands.DeleteClient;
using D2W.Application.Features.Clients.Commands.UpdateClient;
using D2W.Application.Features.Clients.Queries.GetClientForEdit;
using D2W.Application.Features.Clients.Queries.GetClients;
using Microsoft.EntityFrameworkCore;

namespace D2W.Application.UseCases;

public class ClientUseCase : IClientUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly ApplicationUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportingService _reportingService;

    #endregion Private Fields

    #region Public Constructors

    public ClientUseCase(IApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IReportingService reportingService, ApplicationUserManager userManager)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _reportingService = reportingService;
        _userManager = userManager;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<ClientForEdit>> GetClient(GetClientForEditQuery request)
    {
        var client = await _userManager.FindByIdAsync(request.Id);

        if (client == null)
            return Envelope<ClientForEdit>.Result.NotFound(Resource.Unable_to_load_Client);

        var clientForEdit = ClientForEdit.MapFromEntity(client);

        return Envelope<ClientForEdit>.Result.Ok(clientForEdit);
    }

    public async Task<Envelope<ClientsResponse>> GetClients(GetClientsQuery request)
    {
        var query = _dbContext.TenantsClients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(tc => tc.Client.Name.Contains(request.SearchText) || tc.Client.Surname.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(tc => tc.Client.Name).ThenBy(tc => tc.Client.Surname);

        var clientItems = await query
            .Include(q => q.Client.ContactDetails)
            .Select(q => ClientItem.MapFromEntity(q.Client, q.TenantId))
            .AsNoTracking()
            .ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var clientsResponse = new ClientsResponse
        {
            Clients = clientItems
        };

        return Envelope<ClientsResponse>.Result.Ok(clientsResponse);
    }


    // Use register client command instead

    //public async Task<Envelope<RegisterClientResponse>> AddClient(CreateClientCommand request)
    //{
    //    var Client = request.MapToEntity();

    //    await _dbContext.Workrooms.AddAsync(Client);

    //    await _dbContext.SaveChangesAsync();

    //    var createClientResponse = new RegisterClientResponse
    //    {
    //        Id = Client.Id.ToString(),
    //        SuccessMessage = Resource.Client_has_been_created_successfully
    //    };

    //    return Envelope<RegisterClientResponse>.Result.Ok(createClientResponse);
    //}

    public async Task<Envelope<string>> EditClient(UpdateWorkroomCommand request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_ApplicationUser_Id);

        var client = await _userManager.FindByIdAsync(request.Id);

        if (client == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_Client);

        request.MapToEntity(client);

        _dbContext.Users.Update(client);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Client_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteClient(DeleteWorkroomCommand request)
    {

        throw new NotImplementedException();

        //if (string.IsNullOrEmpty(request.Id))
        //    return Envelope<string>.Result.BadRequest(Resource.Invalid_Client_Id);

        //if (!Guid.TryParse(request.Id, out var ClientId))
        //    return Envelope<string>.Result.BadRequest(Resource.Invalid_Client_Id);

        //var Client = await _dbContext.Workrooms.Where(p => p.Id == ClientId).FirstOrDefaultAsync();

        //if (Client == null)
        //    return Envelope<string>.Result.NotFound(Resource.The_Client_is_not_found);

        //_dbContext.Workrooms.Remove(Client);

        //await _dbContext.SaveChangesAsync();

        //return Envelope<string>.Result.Ok(Resource.Client_has_been_deleted_successfully);
    }

    #endregion Public Methods

}
