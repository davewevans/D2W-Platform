namespace D2W.Application.Common.Exceptions;

public class DeleteFailureException : Exception
{
    #region Public Constructors

    public DeleteFailureException(string name, object key, string message) : base(string.Format(Resource.Deletion_of_entity_failed, name, key, message))
    {
    }

    #endregion Public Constructors
}