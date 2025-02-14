namespace eSistem.Proxy.RouteValidatoreSistem;

public sealed class AppModuleRoute
{
    public AppModuleEsistem Module { get; set; } = new();
    public IList<RouteEsistem> Route { get; set; } = [];

}