namespace D2W.Application.Features.AppSettings.Commands.UpdateSettings;

public class UpdateFileStorageSettingsCommand : IRequest<Envelope<TokenSettingsResponse>>
{
    #region Public Properties

    public Guid Id { get; set; }
    public int StorageType { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void MapToEntity(FileStorageSettings fileStorageSettings)
    {
        fileStorageSettings.Id = Id;
        fileStorageSettings.StorageType = (StorageTypes)Enum.Parse(typeof(StorageTypes), StorageType.ToString(), true);
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateFileStorageSettingsCommandHandler : IRequestHandler<UpdateFileStorageSettingsCommand, Envelope<TokenSettingsResponse>>
    {
        #region Private Fields

        private readonly IAppSettingsUseCase _appSettingsUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateFileStorageSettingsCommandHandler(IAppSettingsUseCase appSettingsUseCase)
        {
            _appSettingsUseCase = appSettingsUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<TokenSettingsResponse>> Handle(UpdateFileStorageSettingsCommand request, CancellationToken cancellationToken)
        {
            return await _appSettingsUseCase.EditFileStorageSettings(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}