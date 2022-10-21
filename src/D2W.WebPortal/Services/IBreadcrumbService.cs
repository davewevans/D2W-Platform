namespace D2W.WebPortal.Services;

public interface IBreadcrumbService
{
    #region Public Properties

    List<BreadcrumbItem> BreadcrumbItems { get; set; }

    #endregion Public Properties

    #region Public Methods

    void Reset();

    void SetBreadcrumbItems(List<BreadcrumbItem> breadcrumbItems);

    #endregion Public Methods
}