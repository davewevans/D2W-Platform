namespace D2W.Application.Common.Helpers;

public class ServiceActivator
{
    #region Internal Fields

    internal static IServiceProvider ServiceProvider;

    #endregion Internal Fields

    #region Public Methods

    /// <summary>
    /// Configure ServiceActivator with full serviceProvider
    /// </summary>
    /// <param name="serviceProvider"></param>
    public static void Configure(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// Create a scope where use this ServiceActivator
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <returns></returns>
    public static IServiceScope GetScope(IServiceProvider serviceProvider = null)
    {
        var provider = serviceProvider ?? ServiceProvider;

        return provider?.GetRequiredService<IServiceScopeFactory>().CreateScope();
    }

    #endregion Public Methods
}