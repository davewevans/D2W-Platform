namespace D2W.Application.Features.Identity.Tenants.Commands.CreateTenantCommand;

public class CreateTenantCommand : IRequest<Envelope<CreateTenantResponse>>
{
    #region Public Properties

    public string TenantName { get; set; }
    public string FullName { get; set; }

    #endregion Public Properties

    #region Public Methods

    public Tenant MapToEntity()
    {
        var postfix = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}";
        return new()
        {
            Id = Guid.NewGuid(),
            Name = $"{FullName.Trim().Replace(" ", "-").ToLower()}-{postfix}",
            FullName = FullName,
        };
    }

    #endregion Public Methods

    #region Public Classes

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Envelope<CreateTenantResponse>>
    {
        #region Private Fields

        private readonly ITenantUseCase _roleUseCase;

        #endregion Private Fields

        #region Public Constructors

        public CreateTenantCommandHandler(ITenantUseCase roleUseCase)
        {
            _roleUseCase = roleUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateTenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            return await _roleUseCase.AddTenant(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}