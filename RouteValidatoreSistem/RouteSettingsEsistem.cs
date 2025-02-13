namespace eSistem.Proxy.RouteValidatoreSistem;

internal sealed class RouteModuleModelEsistem
{
    public AppModuleEsistem? ModuleEsistem { get; set; }
    public List<AppRouteModuleEsistem>? AppRouteModulesEsistem { get; set; }

}


internal sealed class RouteSettingsEsistem
{
    public List<RouteModuleModelEsistem> Modules { get; set; } = [];
}