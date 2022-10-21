namespace D2W.Application.Common.Interfaces.UseCases.Identity;

public interface ITenantUseCase
{
    #region Public Methods

    Task<Envelope<CreateTenantResponse>> AddTenant(CreateTenantCommand request);

    #endregion Public Methods
}