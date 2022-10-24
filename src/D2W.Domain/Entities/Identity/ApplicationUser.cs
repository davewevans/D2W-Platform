namespace D2W.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser, IAuditable, IMayHaveTenant
{
    #region Public Constructors

    // TODO one-to-one with WorkroomInfoModel

    public ApplicationUser()
    {
        Claims = new List<ApplicationUserClaim>();
        Logins = new List<ApplicationUserLogin>();
        Tokens = new List<ApplicationUserToken>();
        UserRoles = new List<ApplicationUserRole>();
        UserAttachments = new List<ApplicationUserAttachment>();
    }

    #endregion Public Constructors

    #region Public Properties

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName => $"{Name} {Surname}";
    //public ApplicationUserType AppUserType { get; set; } = ApplicationUserType.Designer;
    public string JobTitle { get; set; }
    public string AvatarUri { get; set; }
    public bool IsSuspended { get; set; }
    public bool IsStatic { get; set; }
    public bool IsDemo { get; set; }
    public bool IsSuperAdmin { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenTimeSpan { get; set; }
    public Guid? TenantId { get; set; }
    public bool IgnoreTenantId { get; set; }
    public List<ApplicationUserClaim> Claims { get; set; }
    public List<ApplicationUserLogin> Logins { get; set; }
    public List<ApplicationUserToken> Tokens { get; set; }
    public List<ApplicationUserRole> UserRoles { get; set; }
    public List<ApplicationUserAttachment> UserAttachments { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    #endregion Public Properties

    #region Navigational Properties

    public ICollection<TenantWorkroomModel> TenantsWorkrooms { get; set; }
    public ICollection<TenantClientModel> TenantsClients { get; set; }
    public ContactDetailsModel ContactDetails { get; set; }

    #endregion
}
