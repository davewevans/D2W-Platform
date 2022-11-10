namespace D2W.Application.Common.Interfaces.Services.DemoUserServices;

public interface IDemoIdentitySeeder
{

    #region Public Methods

    Task<Envelope<ApplicationUser>> SeedDemoOfficersUsers();

    Task SeedDemoClients();
    Task SeedDemoWorkrooms();
    Task SeedDemoFabrics();

    string GetRandomProfilePic();


    #endregion Public Methods
}