namespace D2W.Application.Common.Interfaces.UseCases.POC;

public interface IApplicantUseCase
{
    #region Public Methods

    Task<Envelope<CreateApplicantResponse>> AddApplicant(CreateApplicantCommand request);

    Task<Envelope<string>> EditApplicant(UpdateApplicantCommand request);

    Task<Envelope<ApplicantsResponse>> GetApplicants(GetApplicantsQuery request);

    Task<Envelope<ApplicantForEdit>> GetApplicant(GetApplicantForEditQuery request);

    Task<Envelope<string>> DeleteApplicant(DeleteApplicantCommand request);

    Task<Envelope<ApplicantReferencesResponse>> GetApplicantReferences(GetApplicantReferencesQuery request);

    Task<Envelope<ExportApplicantsResponse>> ExportAsPdf(ExportApplicantsQuery request);

    #endregion Public Methods
}