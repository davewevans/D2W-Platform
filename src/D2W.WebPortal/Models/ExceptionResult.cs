namespace D2W.WebPortal.Models;

public class ExceptionResult
{
    #region Public Properties

    public bool IsError { get; set; }
    public Uri Type { get; set; }
    public string Title { get; set; }
    public long Status { get; set; }
    public string Detail { get; set; }
    public string Instance { get; set; }
    public List<ValidationError> ValidationErrors { get; set; } = new();

    #endregion Public Properties
}

public class ValidationError
{
    #region Public Properties

    public string Name { get; set; }

    public string Reason { get; set; }

    #endregion Public Properties
}