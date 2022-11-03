using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D2W.WebPortal.Features.Identity.Account.Commands.LoginWithCodeCommand
{
    public class LoginWithCodeCommand
    {
        #region Public Properties

        public string Email { get; set; }
        public string TwoFactorCode { get; set; }
        public bool RememberMachine { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string Provider { get; set; }


        #endregion Public Properties
    }
}