using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Domain.Entities;

[Table("Emails")]
public class EmailModel : IAuditable, IMustHaveTenant
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? ClientId { get; set; }

    public Guid? WorkroomId { get; set; }

    public string ToEmailAddress { get; set; }

    public string FromEmailAddress { get; set; }

    public string Subject { get; set; }

    public string TextBody { get; set; }

    public string HtmlBody { get; set; }

    public string Tag { get; set; }

    public string MessageStream { get; set; }

    public EmailStatus Status { get; set; }

    public DateTime? DateSent { get; set; }

    public DateTime? DateReceived { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}
