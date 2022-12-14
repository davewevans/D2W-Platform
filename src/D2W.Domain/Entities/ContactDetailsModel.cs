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
    public class ContactDetailsModel : IAuditable, IMustHaveTenant 
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public string CompanyName { get; set; }

        public string FullName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Phone]
        public string AltPhoneNumber { get; set; }

        [Phone]
        public string Fax { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        
        [EmailAddress]
        public string AltEmailAddress { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public Guid? CountryId { get; set; }

        public string LogoUri { get; set; }
        public string AvatarUri { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }

        #region Navigational Properties

        public ApplicationUser ApplicationUser { get; set; }

        #endregion

    }
}
