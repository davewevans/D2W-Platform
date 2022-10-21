namespace D2W.Domain.Entities.Identity;

public class ApplicationUserAttachment
{
    #region Public Properties

    public Guid Id { get; set; }
    public string FileUri { get; set; }
    public string FileName { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    #endregion Public Properties
}