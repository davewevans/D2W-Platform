namespace D2W.WebAPI.Managers;

public class TimerManager
{
    #region Private Fields

    private Timer _timer;
    private AutoResetEvent _autoResetEvent;
    private Action _action;

    #endregion Private Fields

    #region Public Properties

    public DateTime TimerStarted { get; set; }

    public bool IsTimerStarted { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void PrepareTimer(Action action)
    {
        _action = action;
        _autoResetEvent = new AutoResetEvent(false);
        _timer = new Timer(Execute, _autoResetEvent, 1000, 2000);
        TimerStarted = DateTime.Now;
        IsTimerStarted = true;
    }

    public void Execute(object stateInfo)
    {
        _action();

        if (!((DateTime.Now - TimerStarted).TotalSeconds > 120))
            return;

        IsTimerStarted = false;

        _timer.Dispose();
    }

    #endregion Public Methods
}