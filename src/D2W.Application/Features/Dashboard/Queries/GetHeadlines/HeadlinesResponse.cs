namespace D2W.Application.Features.Dashboard.Queries.GetHeadlines;

public class HeadlinesResponse
{
    #region Private Fields

    private readonly Random _rnd;

    #endregion Private Fields

    #region Public Constructors

    public HeadlinesResponse()
    {
        _rnd = new Random();
    }

    #endregion Public Constructors

    #region Public Properties

    public decimal TotalCost
    {
        get { return TotalCost = _rnd.Next(1, 1000000); }
        set { }
    }

    public decimal GraphiteOnRoof
    {
        get { return GraphiteOnRoof = _rnd.Next(1, 1000000); }
        set { }
    }

    public decimal GlobalSpread
    {
        get { return GlobalSpread = _rnd.Next(1, 1000000); }
        set { }
    }

    public decimal Roentgen
    {
        get { return Roentgen = _rnd.Next(1, 1000000); }
        set { }
    }

    #endregion Public Properties
}