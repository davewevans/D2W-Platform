namespace D2W.WebPortal.Interfaces;

public interface IAppStateManager
{
    #region Public Events

    event EventHandler AppStateChanged;

    #endregion Public Events

    #region Public Properties

    string Message { get; set; }
    object Value { get; set; }
    public bool Visible { get; set; }

    #endregion Public Properties

    #region Public Methods

    void Clear();

    #endregion Public Methods
}