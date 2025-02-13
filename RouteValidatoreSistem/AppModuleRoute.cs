namespace eSistem.Proxy.RouteValidatoreSistem;

internal sealed class AppModuleRoute
{
    public AppModuleEsistem Module { get; set; } = new();
    public IList<RouteEsistem> Route { get; set; } = [];

}