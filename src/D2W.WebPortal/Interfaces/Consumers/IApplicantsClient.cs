namespace D2W.WebPortal.Interfaces.Consumers;

public interface IApplicantsClient
{
    #region Public Methods

    Task<HttpResponseWrapper<object>> GetApplicant(GetApplicantForEditQuery request);

    Task<HttpResponseWrapper<object>> GetApplicantReferences(GetApplicantReferencesQuery request);

    Task<HttpResponseWrapper<object>> GetApplicants(GetApplicantsQuery request);

    Task<HttpResponseWrapper<object>> CreateApplicant(CreateApplicantCommand request);

    Task<HttpResponseWrapper<object>> UpdateApplicant(UpdateApplicantCommand request);

    Task<HttpResponseWrapper<object>> DeleteApplicant(string id);

    Task<HttpResponseWrapper<object>> ExportAsPdf(ExportApplicantsQuery request);

    #endregion Public Methods
}