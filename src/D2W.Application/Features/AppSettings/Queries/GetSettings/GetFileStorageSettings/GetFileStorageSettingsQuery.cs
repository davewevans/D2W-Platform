namespace D2W.Application.Features.AppSettings.Queries.GetSettings.GetFileStorageSettings;

public class GetFileStorageSettingsQuery : IRequest<Envelope<FileStorageSettingsForEdit>>
{
    #region Public Classes

    public class GetFileStorageSettingsQueryHandler : IRequestHandler<GetFileStorageSettingsQuery, Envelope<FileStorageSettingsForEdit>>
    {
        #region Private Fields

        private readonly IAppSettingsUseCase _appSettingsUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetFileStorageSettingsQueryHandler(IAppSettingsUseCase appSettingsUseCase)
        {
            _appSettingsUseCase = appSettingsUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<FileStorageSettingsForEdit>> Handle(GetFileStorageSettingsQuery request, CancellationToken cancellationToken)
        {
            return await _appSettingsUseCase.GetFileStorageSettings();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}