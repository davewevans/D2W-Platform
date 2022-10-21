namespace D2W.Application.Features.Identity.Manage.Commands.DeletePersonalData;

public class DeletePersonalDataCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Password { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeletePersonalDataHandler : IRequestHandler<DeletePersonalDataCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeletePersonalDataHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeletePersonalDataCommand request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.DeletePersonalData(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}