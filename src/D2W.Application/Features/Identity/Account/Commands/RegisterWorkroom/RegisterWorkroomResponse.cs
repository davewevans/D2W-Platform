using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.RegisterWorkroom;

public class RegisterWorkroomResponse
{
    #region Public Properties

    public string Email { get; set; }
    public bool DisplayConfirmAccountLink { get; set; }
    public bool RequireConfirmedAccount { get; set; }
    public string SuccessMessage { get; set; }

    #endregion Public Properties
}
