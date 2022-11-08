using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Workrooms.Queries.GetWorkrooms
{
    public class WorkroomsResponse
    {
         #region Public Properties

        public PagedList<WorkroomItem> Workrooms { get; set; }

        #endregion Public Properties
    }
}