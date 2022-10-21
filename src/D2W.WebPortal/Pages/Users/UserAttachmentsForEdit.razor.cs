namespace D2W.WebPortal.Pages.Users;

public partial class UserAttachmentsForEdit
{
    #region Public Properties

    [Parameter] public List<AssignedUserAttachmentItem> AttachmentsList { get; set; } = new();
    [Parameter] public EventCallback<Guid> OnAttachmentRemoved { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }

    private string SearchText { get; set; }

    #endregion Private Properties

    #region Private Methods

    private bool FilterAttachments(AssignedUserAttachmentItem attachmentItem)
    {
        return string.IsNullOrWhiteSpace(SearchText) || attachmentItem.FileName.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    private async Task RemoveAttachment(AssignedUserAttachmentItem item)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Do_you_want_to_remove_this_record},
            {"ButtonText", Resource.Delete},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Delete, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            AttachmentsList.Remove(item);
            await OnAttachmentRemoved.InvokeAsync(item.Id);
        }
    }

    #endregion Private Methods
}