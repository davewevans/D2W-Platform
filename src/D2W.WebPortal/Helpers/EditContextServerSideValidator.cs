namespace D2W.WebPortal.Helpers;

public class EditContextServerSideValidator : ComponentBase
{
    #region Private Fields

    private ValidationMessageStore _messageStore;

    #endregion Private Fields

    #region Private Properties

    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; }

    #endregion Private Properties

    #region Public Methods

    public void Validate(ExceptionResult exceptionResult)
    {
        _messageStore.Clear();

        if (exceptionResult.ValidationErrors != null)
        {
            foreach (var error in exceptionResult.ValidationErrors)
            {
                _messageStore.Add(CurrentEditContext.Field(error.Name), error.Reason);
            }
        }
        else
        {
            _messageStore.Clear();
            if (!string.IsNullOrEmpty(exceptionResult.Title))
            {
                var fieldIdentifier = new FieldIdentifier(exceptionResult, $" {exceptionResult.Title}");
                _messageStore.Add(fieldIdentifier, $"{exceptionResult.Title}");
            }
            if (!string.IsNullOrEmpty(exceptionResult.Detail))
            {
                var fieldIdentifier = new FieldIdentifier(exceptionResult, $" {exceptionResult.Detail}");
                _messageStore.Add(fieldIdentifier, $"{exceptionResult.Detail}");
            }
        }

        CurrentEditContext.NotifyValidationStateChanged();
    }

    public void Invalidate()
    {
        _messageStore = new ValidationMessageStore(CurrentEditContext);

        CurrentEditContext.OnValidationRequested += (s, e) => _messageStore.Clear();

        CurrentEditContext.OnFieldChanged += (s, e) => _messageStore.Clear(e.FieldIdentifier);

        CurrentEditContext.NotifyValidationStateChanged();
    }

    #endregion Public Methods

    #region Protected Methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException($"{nameof(EditContextServerSideValidator)} requires a cascading " +
                                                $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(EditContextServerSideValidator)} " +
                                                "inside an EditForm.");
        }
        Invalidate();
    }

    #endregion Protected Methods
}