﻿namespace D2W.WebPortal.Consumers;

public class ApplicantsClient : IApplicantsClient
{
    #region Private Fields

    private readonly IHttpService _httpService;

    #endregion Private Fields

    #region Public Constructors

    public ApplicantsClient(IHttpService httpService)
    {
        _httpService = httpService;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<HttpResponseWrapper<object>> GetApplicant(GetApplicantForEditQuery request)
    {
        return await _httpService.Post<GetApplicantForEditQuery, ApplicantForEdit>("applicants/GetApplicant", request);
    }

    public async Task<HttpResponseWrapper<object>> GetApplicantReferences(GetApplicantReferencesQuery getApplicantReferencesQuery)
    {
        return await _httpService.Post<GetApplicantReferencesQuery, ApplicantReferencesResponse>("applicants/GetApplicantReferences", getApplicantReferencesQuery);
    }

    public async Task<HttpResponseWrapper<object>> GetApplicants(GetApplicantsQuery request)
    {
        return await _httpService.Post<GetApplicantsQuery, ApplicantsResponse>("applicants/GetApplicants", request);
    }

    public async Task<HttpResponseWrapper<object>> CreateApplicant(CreateApplicantCommand request)
    {
        return await _httpService.Post<CreateApplicantCommand, CreateApplicantResponse>("applicants/CreateApplicant", request);
    }

    public async Task<HttpResponseWrapper<object>> UpdateApplicant(UpdateApplicantCommand request)
    {
        return await _httpService.Put<UpdateApplicantCommand, string>("applicants/UpdateApplicant", request);
    }

    public async Task<HttpResponseWrapper<object>> DeleteApplicant(string id)
    {
        return await _httpService.Delete<string>($"applicants/DeleteApplicant?id={id}");
    }

    public async Task<HttpResponseWrapper<object>> ExportAsPdf(ExportApplicantsQuery request)
    {
        return await _httpService.Post<ExportApplicantsQuery, ExportApplicantsResponse>("applicants/ExportAsPdf", request);
    }

    #endregion Public Methods
}