using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Workrooms.Queries.GetWorkroomForEdit
{
    public class WorkroomForEdit
    {
        #region Public Properties
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AltEmailAddress { get; set; }
        public string AltPhone1 { get; set; }
        public string ContactName1 { get; set; }
        public string ContactName2 { get; set; }
        public string AvatarUri { get; set; }

        public string AppUserType { get; set; }

        // TODO address

        #endregion Public Properties
    }
}