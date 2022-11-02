using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.LoginWithCode;
public class LoginWithCodeResponse
{
    #region Public Properties

    public string ReturnUrl { get; set; }

    public bool TwoFactorCodeSent { get; set; }
    public AuthResponse AuthResponse { get; set; }

    #endregion Public Properties
}
