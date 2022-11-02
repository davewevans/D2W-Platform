using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.LoginWithCode;
public class LoginWithCodeCommand : IRequest<Envelope<LoginWithCodeResponse>>
{
    #region Public Properties

    public string Email { get; set; }

    [DataType(DataType.Text)]
    public string TwoFactorCode { get; set; }
    public bool RememberMachine { get; set; }
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }
    public string Provider { get; set; }


    #endregion Public Properties

    #region Public Classes

    public class LoginWithCodeCommandHandler : IRequestHandler<LoginWithCodeCommand, Envelope<LoginWithCodeResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public LoginWithCodeCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<LoginWithCodeResponse>> Handle(LoginWithCodeCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.LoginWithCode(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
