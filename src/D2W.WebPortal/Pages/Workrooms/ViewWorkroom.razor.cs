using D2W.WebPortal.Features.Workrooms.Queries.GetWorkroomForEdit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Pages.Workrooms
{
    public partial class ViewWorkroom : ComponentBase
    {
        #region Public Properties

        [Parameter] public string WorkroomId { get; set; }

        #endregion Public Properties

        #region Private Properties

        [Inject] private IBreadcrumbService BreadcrumbService { get; set; }
        [Inject] private IWorkroomsClient WorkroomsWorkroom { get; set; }

        private ServerSideValidator ServerSideValidator { get; set; }
        private WorkroomForEdit WorkroomForEditVm { get; set; } = new();

        #endregion Private Properties

        #region Protected Methods

        protected override async Task OnInitializedAsync()
        {
            BreadcrumbService.SetBreadcrumbItems(new List<BreadcrumbItem>
        {
            new(Resource.Home, "/"),
            new(Resource.Workrooms, "/Workrooms"),
            new(Resource.View_Workroom, "#", true)
        });

            var httpResponseWrapper = await WorkroomsWorkroom.GetWorkroom(new GetWorkroomForEditQuery
            {
                Id = WorkroomId,
            });

            if (httpResponseWrapper.Success)
            {
                var successResult = httpResponseWrapper.Response as SuccessResult<WorkroomForEdit>;
                WorkroomForEditVm = successResult?.Result;
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
