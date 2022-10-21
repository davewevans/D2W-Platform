namespace D2W.Domain.Entities.Identity;

public class ApplicationPermission
{
    #region Public Constructors

    public ApplicationPermission()
    {
        Permissions = new List<ApplicationPermission>();
    }

    #endregion Public Constructors

    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsUserDefined { get; set; }

    public Guid? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public ApplicationPermission Parent { get; set; }

    public IList<ApplicationPermission> Permissions { get; set; }

    #endregion Public Properties
}