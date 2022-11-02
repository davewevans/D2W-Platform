using D2W.Application.Features.Identity.Account.Commands.LoginWithCode;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Identity.Account.Commands.GetLoginVerificationCodeCommand;

public class GetLoginVerificationCodeCommand : IRequest<Envelope<GetLoginVerificationCodeResponse>>
{
    #region Public Properties

    public string Email { get; set; }
    public bool RememberMachine { get; set; }
    public bool RememberMe { get; set; }
    public string ReturnUrl { get; set; }
    public string Provider { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetLoginVerificationCommandHandler : IRequestHandler<GetLoginVerificationCodeCommand, Envelope<GetLoginVerificationCodeResponse>>
    {
        #region Private Fields

        private readonly IAccountUseCase _accountUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetLoginVerificationCommandHandler(IAccountUseCase accountUseCase)
        {
            _accountUseCase = accountUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<GetLoginVerificationCodeResponse>> Handle(GetLoginVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            return await _accountUseCase.GetLoginVerificationCode(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
