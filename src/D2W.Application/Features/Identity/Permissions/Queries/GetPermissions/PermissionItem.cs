namespace D2W.Application.Features.Identity.Permissions.Queries.GetPermissions;

public class PermissionItem : IMapFrom<ApplicationPermission>, IEqualityComparer<PermissionItem>
{
    #region Public Constructors

    public PermissionItem()
    {
        Permissions = new List<PermissionItem>();
    }

    #endregion Public Constructors

    #region Public Properties

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsChecked { get; set; }
    public bool IsExpanded { get; set; }
    public bool IsUserDefined { get; set; }
    public bool HasChildren { get; set; }
    public bool IsRoot { get; set; }

    public List<PermissionItem> Permissions { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static PermissionItem MapFromEntity(ApplicationPermission permission)
    {
        return new()
        {
            Id = permission.Id,
            Name = permission.Name,
            ParentId = permission.ParentId,
            IsRoot = permission.ParentId == null,
            HasChildren = permission.Permissions.Count != 0,
            IsUserDefined = permission.IsUserDefined,
            IsExpanded = permission.Permissions.Count == 0,
            Permissions = permission.Permissions.Select(p => new PermissionItem
            {
                Id = p.Id,
                Name = p.Name,
                ParentId = p.ParentId,
                IsRoot = p.ParentId == null,
                HasChildren = p.Permissions.Count != 0,
                IsUserDefined = p.IsUserDefined,
                IsExpanded = false,
            }).OrderBy(p => p.Name).ToList()
        };
    }

    public bool Equals(PermissionItem x, PermissionItem y)
    {
        if (x == null || y == null)
            return false;

        return ReferenceEquals(x, y) || x.Id == y.Id; // In this example, treat the items as equal if they have the same Id
    }

    public int GetHashCode(PermissionItem obj)
    {
        return Id.GetHashCode();
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationPermission, PermissionItem>()
            .ForMember(dest => dest.HasChildren, opt => opt.MapFrom(src => src.Permissions.Count != 0));

        profile.CreateMap<TreeExtensions.ITree<ApplicationPermission>, PermissionItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Data.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name))
            .ForMember(dest => dest.HasChildren, opt => opt.MapFrom(src => !src.IsLeaf))
            .ForMember(dest => dest.Permissions, opts => opts.MapFrom(src => src.Data.Permissions.OrderBy(p => p.Name)));
    }

    #endregion Public Methods
}