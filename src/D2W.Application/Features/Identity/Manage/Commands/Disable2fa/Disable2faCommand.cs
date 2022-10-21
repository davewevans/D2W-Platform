namespace D2W.Application.Features.Identity.Manage.Commands.Disable2fa;

public class Disable2FaCommand : IRequest<Envelope<string>>
{
    #region Public Classes

    public class Disable2FaCommandHandler : IRequestHandler<Disable2FaCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public Disable2FaCommandHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(Disable2FaCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.Disable2Fa();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}