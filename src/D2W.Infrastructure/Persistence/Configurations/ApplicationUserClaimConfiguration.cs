namespace D2W.Infrastructure.Persistence.Configurations;

public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    #region Public Methods

    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.HasKey(userClaim => new { userClaim.Id });
    }

    #endregion Public Methods
}