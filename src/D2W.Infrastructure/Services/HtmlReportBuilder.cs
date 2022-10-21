namespace D2W.Infrastructure.Services;

public class HtmlReportBuilder : IHtmlReportBuilder
{
    #region Private Fields

    private readonly IWebHostEnvironment _env;

    #endregion Private Fields

    #region Public Constructors

    public HtmlReportBuilder(IWebHostEnvironment env)
    {
        _env = env;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<FileMetaData> GenerateApplicantsPdfFromHtml(string templatePath,
                                                                  string host,
                                                                  string reportMainDirectory,
                                                                  string reportSubDirectory,
                                                                  string reportUserDirectory,
                                                                  IList<ApplicantItem> applicantItems)
    {
        if (string.IsNullOrWhiteSpace(reportUserDirectory))
            throw new ArgumentNullException(nameof(reportUserDirectory));

        var templateFile = await File.ReadAllTextAsync(GetPath("HTMLTemplates", "Applicants.html"));

        var builder = new StubbleBuilder();

        var boundTemplate = await builder.Build().RenderAsync(templateFile, new { Applicants = applicantItems, Logo = "Logo" });

        var physicalDirectoryPath = Path.Combine(Path.Combine(Path.Combine(_env.WebRootPath, reportMainDirectory), reportSubDirectory), reportUserDirectory);

        var relativeDirectoryPath = Path.Combine(Path.Combine(reportMainDirectory, reportSubDirectory), reportUserDirectory);

        Directory.CreateDirectory(physicalDirectoryPath);

        var unixTimeSeconds = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

        var newFileName = $"{"Applicants"}-{reportUserDirectory}-{unixTimeSeconds}.html";

        var physicalFilePath = Path.Combine(physicalDirectoryPath, newFileName);

        await File.WriteAllTextAsync(physicalFilePath, boundTemplate);

        var relativeFilePath = Path.Combine(relativeDirectoryPath, newFileName);

        var url = $"{host}/{relativeFilePath}";

        var fileMetaData = await RenderAsPdf("Applicants", host, reportUserDirectory, unixTimeSeconds, physicalDirectoryPath, relativeDirectoryPath, url);

        return new FileMetaData
        { FileName = newFileName.Replace(".html", ".pdf"), FileUri = fileMetaData.FileUri, ContentType = fileMetaData.ContentType };
    }

    public string GetPath(string containerName, string fileName)
    {
        return Path.Combine(_env.WebRootPath, Path.Combine(containerName, fileName));
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task<FileMetaData> RenderAsPdf(string reportTitle,
                                                        string host,
                                                        string reportUserDirectory,
                                                        long unixTimeSeconds,
                                                        string physicalDirectoryPath,
                                                        string relativeDirectoryPath,
                                                        string url)
    {
        var newPdfFileName = $"{reportTitle}-{reportUserDirectory}.{unixTimeSeconds}.pdf";

        var physicalPdfFilePath = Path.Combine(physicalDirectoryPath, newPdfFileName);

        var relativePdfFilePath = Path.Combine(relativeDirectoryPath, newPdfFileName);

        var pdfUrl = $"{host}/{relativePdfFilePath}";

        var renderer = new ChromePdfRenderer();

        using var pdf = await renderer.RenderUrlAsPdfAsync(url);

        pdf.SaveAs(physicalPdfFilePath);

        return new FileMetaData { FileName = newPdfFileName, FileUri = pdfUrl, ContentType = MediaTypeNames.Application.Pdf };
    }

    #endregion Private Methods
}