namespace D2W.Infrastructure.Services
{
    public class DataExportService : IDataExportService
    {
        #region Private Fields

        private readonly IWebHostEnvironment _env;

        #endregion Private Fields

        #region Public Constructors

        public DataExportService(IWebHostEnvironment env)
        {
            _env = env;
        }

        #endregion Public Constructors

        #region Public Methods

        public string CreateSpreadsheetWorkbook(string fileName)
        {
            var directory = Path.Combine(_env.WebRootPath, "Spreadsheets");

            Directory.CreateDirectory(directory);

            var physicalPath = Path.Combine(directory, fileName);

            // Create a spreadsheet document by supplying the filepath. By default, AutoSave = true,
            // Editable = true, and Type = xlsx.
            var spreadsheetDocument = SpreadsheetDocument
                .Create(physicalPath, SpreadsheetDocumentType.Workbook);

            // Add a WorkbookPart to the document.
            var workbookpart = spreadsheetDocument.AddWorkbookPart();
            workbookpart.Workbook = new Workbook();

            // Add a WorksheetPart to the WorkbookPart.
            var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            // Add Sheets to the Workbook.
            var sheets = spreadsheetDocument.WorkbookPart.Workbook.
                AppendChild(new Sheets());

            // Append a new worksheet and associate it with the workbook.
            var sheet = new Sheet()
            {
                Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "mySheet"
            };
            sheets.Append(sheet);

            workbookpart.Workbook.Save();

            // Close the document.
            spreadsheetDocument.Close();

            var relativePath = $"Spreadsheets/{fileName}";

            return relativePath;
        }

        #endregion Public Methods
    }
}