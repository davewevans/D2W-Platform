namespace D2W.Application.Common.Interfaces.Services;

public interface IHtmlReportBuilder
{
    #region Public Methods

    string GetPath(string containerName, string fileName);

    Task<FileMetaData> GenerateApplicantsPdfFromHtml(string templatePath, string host, string reportMainDirectory, string reportSubDirectory, string reportUserDirectory, IList<ApplicantItem> applicantItems);

    #endregion Public Methods
}