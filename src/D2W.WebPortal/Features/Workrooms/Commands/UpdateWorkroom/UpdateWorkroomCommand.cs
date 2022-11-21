using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Workrooms.Commands.UpdateWorkroom
{
    public class UpdateWorkroomCommand
    {
        #region Public Properties
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
        public string AltEmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string AltPhoneNumber { get; set; }
        public string Fax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public Guid? CountryId { get; set; }

        #endregion Public Properties
    }
}