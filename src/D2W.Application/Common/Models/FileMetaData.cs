namespace D2W.Application.Common.Models;

public class FileMetaData
{
    #region Public Properties

    public string FileName { get; set; }
    public string FileUri { get; set; }
    public string ContentType { get; set; }
    public bool IsDefault { get; set; }

    #endregion Public Properties
}