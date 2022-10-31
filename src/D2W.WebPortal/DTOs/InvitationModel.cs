using System.ComponentModel.DataAnnotations.Schema;

namespace D2W.WebPortal.DTOs
{
    [Table(name: "InvitationCodes")]
    public class InvitationModel : Entity
    {
        public InvitationModel(string invitationCode, string inviteeEmail, string inviteeFirstName, string inviteeLastName)
        {
            InvitationCode = invitationCode;
            InviteeEmail = inviteeEmail;
            InviteeFirstName = inviteeFirstName;
            InviteeLastName = inviteeLastName;
        }

        public string InvitationCode { get; set; } 

        public string InviteeEmail { get; set; } 

        public string InviteeFirstName { get; set; } 

        public string InviteeLastName { get; set; } 

        public Guid DesignId { get; set; }

        public bool IsComplete { get; set; }

        public AppUserRole Role { get; set; }
    }
}
