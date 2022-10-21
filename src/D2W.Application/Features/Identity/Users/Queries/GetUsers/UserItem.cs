namespace D2W.Application.Features.Identity.Users.Queries.GetUsers;

public class UserItem : AuditableDto
{
    #region Public Properties

    public string Id { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string FullName { get; set; }
    public string JobTitle { get; set; }
    public string AvatarUri { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsSuspended { get; set; }
    public bool IsStatic { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static UserItem MapFromEntity(ApplicationUser u)
    {
        return new()
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email,
            Name = u.Name,
            FullName = u.FullName,
            Surname = u.Surname,
            JobTitle = u.JobTitle,
            AvatarUri = u.AvatarUri,
            IsSuspended = u.IsSuspended,
            IsStatic = u.IsStatic,
            EmailConfirmed = u.EmailConfirmed,
            CreatedOn = u.CreatedOn,
            CreatedBy = u.CreatedBy,
            ModifiedOn = u.ModifiedOn,
            ModifiedBy = u.ModifiedBy
        };
    }

    #endregion Public Methods
}