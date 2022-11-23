using D2W.WebPortal.Features.Fabrics.Queries.GetFabricForEdit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Fabrics
{
    public partial class ViewFabric : ComponentBase
    {
        #region Public Properties

        [Parameter] public Guid FabricId { get; set; }

        #endregion Public Properties

        #region Private Properties

        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IFabricsClient FabricsFabric { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private FabricForEdit FabricForEditVm { get; set; } = new();

        #endregion Private Properties

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Fabrics, "/Fabrics"),
            new(Resource.View_Fabric, "#", true)
        });

            var httpResponseWrapper = await FabricsFabric.GetFabric(new GetFabricForEditQuery
            {
                Id = FabricId,
            });

            if (httpResponseWrapper.Success)
            {
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
    }
}
