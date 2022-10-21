namespace D2W.Application.Common.Mappings;

public interface IMapFrom<T>
{
    #region Public Methods

    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());

    #endregion Public Methods
}