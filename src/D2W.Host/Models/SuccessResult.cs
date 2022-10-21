namespace D2W.HostApp.Models;

public class SuccessResult<T>
{
    #region Public Properties

    public int StatusCode { get; set; }
    public string Message { get; set; }
    public DateTime RequestDate { get; set; }
    public string ApiVersion { get; set; }
    public T Result { get; set; }

    #endregion Public Properties
}