using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Domain.Entities.Identity;

namespace D2W.Domain.Entities
{
    [Table("TenantsWorkrooms")]
    public class TenantWorkroomModel : IAuditable, IMustHaveTenant
    {
        public string ApplicationUserId { get; set; }
        public Guid TenantId { get; set; }
    
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        #region Navigational Properties

        public ApplicationUser Workroom { get; set; }
        public Tenant Tenant { get; set; }

        #endregion
    }
}
