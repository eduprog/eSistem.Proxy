namespace eSistem.Proxy.RouteValidatoreSistem;

internal class RouteSettingService
{
    public RouteEndpointSettings RouteEndpointSettings { get; } = new();

    public RouteSettingService(IConfiguration configuration)
    {
        // Carrega as configurações do JSON
        var modules = configuration.GetSection("RouteSettings:Modules").Get<List<RouteModuleModelEsistem>>();

        if (modules != null)
        {
            RouteEndpointSettings.RouteEndpointeSistem.AddRange(modules);
        }
    }
}