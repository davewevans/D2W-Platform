using D2W.Application.Common.Managers;

namespace D2W.Application.UseCases.POC;

public class ApplicantUseCase : IApplicantUseCase
{
    #region Private Fields

    private readonly IApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportingService _reportingService;

    #endregion Private Fields

    #region Public Constructors

    public ApplicantUseCase(IApplicationDbContext dbContext,
                            IHttpContextAccessor httpContextAccessor,
                            IReportingService reportingService)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _reportingService = reportingService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<Envelope<ApplicantForEdit>> GetApplicant(GetApplicantForEditQuery request)
    {
        if (!Guid.TryParse(request.Id, out var applicantId))
            return Envelope<ApplicantForEdit>.Result.BadRequest(Resource.Invalid_applicant_Id);

        var applicant = await _dbContext.Applicants.Where(a => a.Id == applicantId).FirstOrDefaultAsync();

        if (applicant == null)
            return Envelope<ApplicantForEdit>.Result.NotFound(Resource.Unable_to_load_applicant);

        var applicantForEdit = ApplicantForEdit.MapFromEntity(applicant);

        return Envelope<ApplicantForEdit>.Result.Ok(applicantForEdit);
    }

    public async Task<Envelope<ApplicantsResponse>> GetApplicants(GetApplicantsQuery request)
    {
        var query = _dbContext.Applicants.Include(a => a.References).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
            query = query.Where(a => a.FirstName.Contains(request.SearchText) || a.LastName.Contains(request.SearchText));

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(a => a.FirstName).ThenBy(a => a.LastName);

        var applicantItems = await query.Select(q => ApplicantItem.MapFromEntity(q)).AsNoTracking().ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var applicantsResponse = new ApplicantsResponse
        {
            Applicants = applicantItems
        };

        return Envelope<ApplicantsResponse>.Result.Ok(applicantsResponse);
    }

    public async Task<Envelope<CreateApplicantResponse>> AddApplicant(CreateApplicantCommand request)
    {
        var applicant = request.MapToEntity();

        await _dbContext.Applicants.AddAsync(applicant);

        await _dbContext.SaveChangesAsync();

        var createApplicantResponse = new CreateApplicantResponse
        {
            Id = applicant.Id.ToString(),
            SuccessMessage = Resource.Applicant_has_been_created_successfully
        };

        return Envelope<CreateApplicantResponse>.Result.Ok(createApplicantResponse);
    }

    public async Task<Envelope<string>> EditApplicant(UpdateApplicantCommand request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_applicant_Id);

        if (!Guid.TryParse(request.Id, out var applicantId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_applicant_Id);

        var applicant = await _dbContext.Applicants.Include(a => a.References).Where(a => a.Id == applicantId).FirstOrDefaultAsync();

        if (applicant == null)
            return Envelope<string>.Result.NotFound(Resource.Unable_to_load_applicant);

        await request.MapToEntity(applicant, _dbContext);

        _dbContext.Applicants.Update(applicant);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Applicant_has_been_updated_successfully);
    }

    public async Task<Envelope<string>> DeleteApplicant(DeleteApplicantCommand request)
    {
        if (string.IsNullOrEmpty(request.Id))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_applicant_Id);

        if (!Guid.TryParse(request.Id, out var applicantId))
            return Envelope<string>.Result.BadRequest(Resource.Invalid_applicant_Id);

        var applicant = await _dbContext.Applicants.Where(p => p.Id == applicantId).FirstOrDefaultAsync();

        if (applicant == null)
            return Envelope<string>.Result.NotFound(Resource.The_applicant_is_not_found);

        _dbContext.Applicants.Remove(applicant);

        await _dbContext.SaveChangesAsync();

        return Envelope<string>.Result.Ok(Resource.Applicant_has_been_deleted_successfully);
    }

    public async Task<Envelope<ApplicantReferencesResponse>> GetApplicantReferences(GetApplicantReferencesQuery request)
    {
        var query = _dbContext.References.Where(a => a.ApplicantId == request.ApplicantId
                                                     && (a.Name.Contains(request.SearchText)
                                                     || a.JobTitle.Contains(request.SearchText)
                                                     || a.Phone.Contains(request.SearchText)
                                                     || string.IsNullOrEmpty(request.SearchText)));

        query = !string.IsNullOrWhiteSpace(request.SortBy)
            ? query.SortBy(request.SortBy)
            : query.OrderBy(a => a.Name);

        var applicantReferenceItems = await query.Select(q => ApplicantReferenceItem.MapFromEntity(q)).ToPagedListAsync(request.PageNumber, request.RowsPerPage);

        var applicantReferencesResponse = new ApplicantReferencesResponse
        {
            ApplicantReferences = applicantReferenceItems
        };

        return Envelope<ApplicantReferencesResponse>.Result.Ok(applicantReferencesResponse);
    }

    public async Task<Envelope<ExportApplicantsResponse>> ExportAsPdf(ExportApplicantsQuery request)
    {
        var applicantResponse = await GetApplicants(new GetApplicantsQuery { SearchText = request.SearchText, SortBy = request.SortBy });

        var currentUserName = _httpContextAccessor.GetUserName();

        var issuerName = currentUserName.Split("@")[0];

        var host = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

        var payload = await _reportingService.ExportApplicantsAsPdfImmediate(applicantResponse.Payload, host, issuerName);

        return Envelope<ExportApplicantsResponse>.Result.Ok(payload);
    }

    #endregion Public Methods
}