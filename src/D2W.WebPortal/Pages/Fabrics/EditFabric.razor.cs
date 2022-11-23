using D2W.WebPortal.Features.Fabrics.Commands.UpdateFabric;
using D2W.WebPortal.Features.Fabrics.Queries.GetFabricForEdit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Fabrics
{
    public partial class EditFabric : ComponentBase
    {
        #region Public Properties

        [Parameter] public Guid FabricId { get; set; }

        #endregion Public Properties

        #region Private Properties

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IFabricsClient FabricsFabric { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private EditContextServerSideValidator EditContextServerSideValidator { get; set; }
        private FabricForEdit FabricForEditVm { get; set; } = new();
        private UpdateFabricCommand UpdateFabricCommand { get; set; }

        #endregion Private Properties

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
            {
                new(Resource.Home, "/"),
                new(Resource.Fabrics, "fabrics"),
                new(Resource.Edit_Fabric, "#", true)
            });

            System.Console.WriteLine("fabric id: " + FabricId);

            var httpResponseWrapper = await FabricsFabric.GetFabric(new GetFabricForEditQuery
            {
                Id = FabricId,
            });

            if (httpResponseWrapper.Success)
            {
                System.Console.WriteLine("httpResponseWrapper.Success");
                var successResult = httpResponseWrapper.Response as SuccessResult<FabricForEdit>;
                FabricForEditVm = successResult?.Result;
            }
            else
            {
                var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
                ServerSideValidator.Validate(exceptionResult);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private async Task SubmitForm()
        {
            UpdateFabricCommand = new UpdateFabricCommand
            {
                ManufacturerName = FabricForEditVm.ManufacturerName,
                BrandName = FabricForEditVm.BrandName,
                MaterialType = FabricForEditVm.MaterialType,
                ProductNumber = FabricForEditVm.ProductNumber,
                Pattern = FabricForEditVm.Pattern,
                Color = FabricForEditVm.Color,
                SwatchImageUri = FabricForEditVm.SwatchImageUri,
                CostPerYard = FabricForEditVm.CostPerYard,
                CostPerMeter = FabricForEditVm.CostPerMeter,
                IsRepeating = FabricForEditVm.IsRepeating,
                VerticalRepeatInInches = FabricForEditVm.VerticalRepeatInInches,
                VerticalRepeatInCentimeters = FabricForEditVm.VerticalRepeatInCentimeters,
                HorizontalRepeatInInches = FabricForEditVm.HorizontalRepeatInInches,
                HorizontalRepeatInCentimeters = FabricForEditVm.HorizontalRepeatInCentimeters,
                WidthInInches = FabricForEditVm.WidthInInches,
                WidthInCentimeters = FabricForEditVm.WidthInCentimeters,
            };
            var httpResponse = await FabricsFabric.UpdateFabric(UpdateFabricCommand);

            if (httpResponse.Success)
            {
                var successResult = httpResponse.Response as SuccessResult<string>;
                Snackbar.Add(successResult.Result, Severity.Success);
                NavigationManager.NavigateTo("fabrics");
            }
            else
            {
                var exceptionResult = httpResponse.Response as ExceptionResult;
                EditContextServerSideValidator.Validate(exceptionResult);
                ServerSideValidator.Validate(exceptionResult);
            }
        }
        #endregion Private Methods
    }
}
