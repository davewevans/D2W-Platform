using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand
{
    public class SendLoginVerificationCodeResponse
    {
        #region Public Properties

        public string ReturnUrl { get; set; }

        public bool TwoFactorCodeSent { get; set; }

        #endregion Public Properties
    }
}