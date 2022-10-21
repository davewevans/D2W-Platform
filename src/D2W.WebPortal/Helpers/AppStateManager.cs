namespace D2W.WebPortal.Helpers;

public class AppStateManager : IAppStateManager
{
    #region Private Fields

    private bool _visible;

    #endregion Private Fields

    #region Public Events

    public event EventHandler AppStateChanged;

    #endregion Public Events

    #region Public Properties

    public string Message { get; set; }
    public object Value { get; set; }

    public bool Visible
    {
        get => _visible;
        set
        {
            _visible = value;
            AppStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    #endregion Public Properties

    #region Public Methods

    public void Clear()
    {
        Message = null;
    }

    #endregion Public Methods
}