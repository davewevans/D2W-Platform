namespace D2W.WebPortal.Pages.POC.Army;

public partial class ApplicantReferencesListForAdd
{
    #region Public Properties

    [Parameter] public EventCallback<List<ReferenceItemForAdd>> OnApplicantReferencesChanged { get; set; }

    public ReferenceItemForEdit ReferenceItemForEdit { get; set; }

    #endregion Public Properties

    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }

    private string SearchString { get; set; }
    private List<ReferenceItemForAdd> AddedApplicantReferencesList { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private bool FilterReferences(ReferenceItemForAdd item)
    {
        return string.IsNullOrWhiteSpace(SearchString) || item.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase);
    }

    private async Task AddApplicantReference()
    {
        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Large };

        var dialog = DialogService.Show<AddApplicantReferenceFormDialog>(Resource.Assigned_Roles, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            SearchString = null;
            AddedApplicantReferencesList.Add((ReferenceItemForAdd)result.Data);
            await OnApplicantReferencesChanged.InvokeAsync(AddedApplicantReferencesList);
        }
    }

    private async Task EditApplicantReference(string id)
    {
        var referenceCommandParam = AddedApplicantReferencesList.Select(a =>
            new ReferenceItemForEdit
            {
                Id = a.Id,
                JobTitle = a.JobTitle,
                Name = a.Name,
                Phone = a.Phone,
            }).FirstOrDefault(i => i.Id == id);

        var parameters = new DialogParameters { ["ReferenceItemForEdit"] = referenceCommandParam };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Large };

        var dialog = DialogService.Show<EditApplicantReferenceFormDialog>(Resource.Add_Reference, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            SearchString = null;
            ReferenceItemForEdit = (ReferenceItemForEdit)result.Data;

            var modifiedReferenceIndex = AddedApplicantReferencesList.ToList()
                .FindIndex(item => item.Id == ReferenceItemForEdit.Id);

            if (modifiedReferenceIndex >= 0)
            {
                AddedApplicantReferencesList[modifiedReferenceIndex] = new ReferenceItemForAdd
                {
                    Id = ReferenceItemForEdit.Id,
                    JobTitle = ReferenceItemForEdit.JobTitle,
                    Name = ReferenceItemForEdit.Name,
                    Phone = ReferenceItemForEdit.Phone,
                };
            }
        }
    }

    private async Task RemoveApplicantReference(string id)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", Resource.Do_you_want_to_remove_this_record},
            {"ButtonText", Resource.Remove},
            {"Color", Color.Error}
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = DialogService.Show<DialogModal>(Resource.Remove, parameters, options);

        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            var item = AddedApplicantReferencesList.Select(a =>
                new ReferenceItemForAdd
                {
                    Id = a.Id,
                    JobTitle = a.JobTitle,
                    Name = a.Name,
                    Phone = a.Phone,
                }).FirstOrDefault(i => i.Id == id);

            AddedApplicantReferencesList.Remove(item);

            await OnApplicantReferencesChanged.InvokeAsync(AddedApplicantReferencesList);
        }
    }

    #endregion Private Methods
}