namespace D2W.Application.Common.Interfaces.Services;

public interface IDataExportService
{
    #region Public Methods

    string CreateSpreadsheetWorkbook(string fileName);

    #endregion Public Methods
}