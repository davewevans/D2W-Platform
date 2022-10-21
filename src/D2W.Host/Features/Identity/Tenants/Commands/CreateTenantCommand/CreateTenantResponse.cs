﻿namespace D2W.HostApp.Features.Identity.Tenants.Commands.CreateTenantCommand;

public class CreateTenantResponse
{
    #region Public Properties

    public Guid Id { get; internal set; }
    public string SuccessMessage { get; internal set; }

    #endregion Public Properties
}