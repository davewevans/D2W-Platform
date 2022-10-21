namespace D2W.WebPortal.Pages.POC.Army
{
    public partial class AddApplicantReferenceFormDialog
    {
        #region Public Properties

        [Parameter] public ReferenceItemForAdd ReferenceItemForAdd { get; set; } = new();

        #endregion Public Properties

        #region Private Properties

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }

        #endregion Private Properties

        #region Private Methods

        private void SubmitForm()
        {
            ReferenceItemForAdd.Id = Guid.NewGuid().ToString();
            MudDialog.Close(DialogResult.Ok(ReferenceItemForAdd));
        }

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        #endregion Private Methods
    }
}