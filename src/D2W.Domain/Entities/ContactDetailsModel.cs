using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using D2W.Domain.Entities.Identity;

namespace D2W.Domain.Entities
{
    [Table("ContactDetails")]
    public class ContactDetailsModel : IAuditable
    {
        public Guid Id { get; set; }

        [Phone]
        public string AltPhone1 { get; set; }

        [Phone]
        public string AltPhone2 { get; set; }

        [Phone]
        public string Fax { get; set; }

        [EmailAddress]
        public string AltEmailAddress1 { get; set; }
        
        [EmailAddress]
        public string AltEmailAddress2 { get; set; }

        public string ContactName1 { get; set; }
        public string ContactName2 { get; set; }
        public string ContactName3 { get; set; }
        public string ContactName4 { get; set; }
        public string ContactName5 { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public Guid CountryId { get; set; }
        public string LogoUri { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        public string ApplicationUserId { get; set; }

        #region Navigational Properties

        public ApplicationUser ApplicationUser { get; set; }

        #endregion
    }
}
