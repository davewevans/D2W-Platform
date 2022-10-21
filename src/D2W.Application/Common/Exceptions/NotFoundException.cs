namespace D2W.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    #region Public Constructors

    public NotFoundException(string name, object key) : base(string.Format(Resource.Entity_with_key_was_not_found, name, key))
    {
    }

    #endregion Public Constructors
}