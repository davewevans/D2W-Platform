namespace D2W.WebPortal.Services;

public class BreadcrumbService : IBreadcrumbService
{
    #region Public Properties

    public List<BreadcrumbItem> BreadcrumbItems { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void Reset()
    {
        BreadcrumbItems = new List<BreadcrumbItem>();
    }

    public void SetBreadcrumbItems(List<BreadcrumbItem> breadcrumbItems)
    {
        BreadcrumbItems = new List<BreadcrumbItem>();
        BreadcrumbItems = breadcrumbItems;
    }

    #endregion Public Methods
}