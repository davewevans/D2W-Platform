using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.GetLoginVerificationCodeCommand;

public class GetLoginVerificationCodeResponse
{
    #region Public Properties

    public string ReturnUrl { get; set; }

    public bool TwoFactorCodeSent { get; set; }

    #endregion Public Properties
}
