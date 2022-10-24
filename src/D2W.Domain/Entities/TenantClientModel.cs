using D2W.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities
{
    [Table("TenantsClients")]
    public class TenantClientModel : IAuditable, IMustHaveTenant
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

        public ApplicationUser Client { get; set; }
        public Tenant Tenant { get; set; }

        #endregion
    }
}
