namespace D2W.WebPortal.Models;

public abstract class FilterableQuery
{
    #region Public Properties

    public string SearchText { get; set; }

    public string SortBy { get; set; }

    public int PageNumber { get; set; }

    /// <summary>
    /// The or set the maximum total number of records for each page.
    /// </summary>
    public int RowsPerPage { get; set; }

    /// <summary>
    /// The or set the actual total number of records of the selected page.
    /// </summary>
    public int SelectedPageSize { get; set; }

    #endregion Public Properties
}