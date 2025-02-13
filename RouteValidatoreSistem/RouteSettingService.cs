namespace eSistem.Proxy.RouteValidatoreSistem;

internal class RouteSettingService
{
    public RouteSettingsEsistem RouteSettingsEsistem { get; }

    public RouteSettingService(IConfiguration configuration)
    {
        // Carrega as configurações do JSOn
        RouteSettingsEsistem = configuration.GetSection("RouteSettingsEsistem").Get<RouteSettingsEsistem>()
                               ?? new RouteSettingsEsistem();

        
    }
}