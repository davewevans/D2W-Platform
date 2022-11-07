namespace D2W.Application.Common.Interfaces.Persistence;

public interface IApplicationDbContext : IDisposable
{
    #region Public Properties

    DbSet<ApplicationUser> Users { get; set; }
    DbSet<ApplicationRole> Roles { get; set; }
    DbSet<ApplicationUserRole> UserRoles { get; set; }
    DbSet<ApplicationUserClaim> UserClaims { get; set; }
    DbSet<ApplicationUserLogin> UserLogins { get; set; }
    DbSet<ApplicationRoleClaim> RoleClaims { get; set; }
    DbSet<ApplicationUserToken> UserTokens { get; set; }
    DbSet<ApplicationUserAttachment> ApplicationUserAttachments { get; set; }
    DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
    DbSet<Tenant> Tenants { get; set; }
    DbSet<UserSettings> UserSettings { get; set; }
    DbSet<PasswordSettings> PasswordSettings { get; set; }
    DbSet<LockoutSettings> LockoutSettings { get; set; }
    DbSet<SignInSettings> SignInSettings { get; set; }
    DbSet<TokenSettings> TokenSettings { get; set; }
    DbSet<FileStorageSettings> FileStorageSettings { get; set; }
    DbSet<Applicant> Applicants { get; set; }
    DbSet<Reference> References { get; set; }
    DbSet<Report> Reports { get; set; }

    DbContext Current { get; }
    DatabaseFacade Database { get; }

    DbSet<TenantWorkroomModel> TenantsWorkrooms { get; set; }
    DbSet<TenantClientModel> TenantsClients { get; set; }
    DbSet<ContactDetailsModel> ContactDetails { get; set; }
    DbSet<CountryModel> Countries { get; set; }
    DbSet<DesignConceptModel> DesignConcepts { get; set; }
    DbSet<WorkOrderModel> WorkOrders { get; set; }
    DbSet<WindowMeasurementsModel> WindowMeasurements { get; set; }
    DbSet<DraperyCalculationsModel> DraperyCalculations { get; set; }


    #endregion Public Properties

    #region Public Methods

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    void SetTargetTenantId(Guid tenantId);

    #endregion Public Methods
}