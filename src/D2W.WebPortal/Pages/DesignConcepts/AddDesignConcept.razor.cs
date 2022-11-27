using D2W.WebPortal.Features.Clients.Queries.GetClients;
using D2W.WebPortal.Features.DesignConcepts.Commands.CreateDesignConceptCommand;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.DesignConcepts
{
    public partial class AddDesignConcept : ComponentBase
    {
        #region Private Properties

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IDesignConceptsClient DesignConceptsClient { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IAppStateManager AppStateManager { get; set; }
        [Inject] private IClientsClient ClientsClient { get; set; }
        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
        private CreateDesignConceptCommand CreateDesignConceptCommand { get; set; } = new();


        private ClientItem? _selectedClient = null;
        private ClientsResponse _clientsForAutoResponse = new();
        private GetClientsQuery _getClientsQuery { get; set; } = new();

        private DraperyCalculationsItemForAdd _draperyCalculationsItem = new();

        private string? _designConceptImageSrc;

        private StreamContent? _imageContent { get; set; }

        private string _windowIllustrationImageSrc = "images/20221109_080149.jpg";

        private bool IsTipsOpen { get; set; }

        #endregion Private Properties

        #region Protected Methods

        protected override void OnInitialized()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
            {
                new(Resource.Home, "/"),
                new(Resource.Design_Concepts, "DesignConcepts"),
                new(Resource.Add_Design_Concept, "#", true)
            });


        }

        #endregion Protected Methods

        #region Private Methods

        private void TipsToggle()
        {
            IsTipsOpen = !IsTipsOpen;
        }

        private async void AppStateChanged(object sender, EventArgs args)
        {
            await InvokeAsync(StateHasChanged);
        }

        private void ClientSelectedHandler(ClientItem value)
        {
            _selectedClient = value;
            CreateDesignConceptCommand.ClientId = _selectedClient?.Id;
            StateHasChanged();
        }

        private async Task<IEnumerable<ClientItem>> SearchClients(string? value)
        {
            System.Console.WriteLine("client auto complete value: " + value ?? "value is null");

            _getClientsQuery.SearchText = string.IsNullOrEmpty(value) ? string.Empty : value;

            _getClientsQuery.PageNumber = 1;

            _getClientsQuery.RowsPerPage = 1000;

            var responseWrapper = await ClientsClient.GetClients(_getClientsQuery);

            if (responseWrapper.Success)
            {
                var successResult = responseWrapper.Response as SuccessResult<ClientsResponse>;
                if (successResult != null)
                    _clientsForAutoResponse = successResult.Result;
            }
            else
            {
                var exceptionResult = responseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }

            return _clientsForAutoResponse.Clients.Items;
        }

        private IEnumerable<string> ValidateClient(string? value)
        {
            var client = _clientsForAutoResponse?.Clients?.Items.FirstOrDefault(a => a.FullName.Equals(value));
            if (client is null)
            {
                yield return "No client found";
            }
        }

        private void RemoveDesignConceptImage()
        {
            _imageContent = null;
            _designConceptImageSrc = null;
            if (CreateDesignConceptCommand?.ImageUrl is not null)
            {
                CreateDesignConceptCommand.ImageUrl = null;
            }
            StateHasChanged();
        }

        private bool HasUploadedImage()
        {
            return !string.IsNullOrWhiteSpace(_designConceptImageSrc);
        }

        private void ImageSelected(StreamContent content)
        {
            _imageContent = content;
            //CreateDesignConceptCommand.IsImageAdded = true;
            StateHasChanged();
        }

        private void GetBase64StringImageUrl(string imageSrc)
        {
            _designConceptImageSrc = imageSrc;
            StateHasChanged();
        }

        private void ImageUnSelected()
        {
            _imageContent = null;
            //CreateDesignConceptCommand.IsImageAdded = false;
            StateHasChanged();
        }

        private async Task SubmitForm()
        {
            var httpResponseWrapper = await DesignConceptsClient.CreateDesignConcept(CreateDesignConceptCommand);

            System.Console.WriteLine($"http response: {httpResponseWrapper.Success}");

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<CreateDesignConceptResponse>;

                if (successResult is not null)
                {
                    Snackbar.Add(successResult.Result.SuccessMessage, Severity.Success);
                }
                else
                {
                    Snackbar.Add("result is null", Severity.Error);
                }

                NavigationManager.NavigateTo("DesignConcepts");
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }

        #endregion Private Methods
    }
}
