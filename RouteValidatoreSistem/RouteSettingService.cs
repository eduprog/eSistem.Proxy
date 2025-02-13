namespace eSistem.Proxy.RouteValidatoreSistem;

internal class RouteSettingService
{
    public RouteSettingService(IConfiguration configuration)
    {
        RouteModules = configuration.GetSection("RouteModules")
                                .Get<RouteModules>()
                            ?? new RouteModules();
        CleanRouteAndNameEmpty();
    }

    private void CleanRouteAndNameEmpty()
    {
        // Criar uma lista para armazenar os módulos que precisam ser removidos
        var modulesToRemove = new List<AppModuleRoute>();

        foreach (var module in RouteModules.Modules)
        {

            // Remover rotas inválidas (Path vazio)
            module.Route = module.Route
                .Where(routeEsistem => !string.IsNullOrWhiteSpace(routeEsistem.Path))
                .ToList();

            // Se o módulo não tem rotas ou o nome está vazio, marcar para remoção
            if (module.Route.Count == 0 || string.IsNullOrWhiteSpace(module.Module.Name))
            {
                modulesToRemove.Add(module);
            }
        }

        // Remover os módulos marcados de uma vez
        foreach (var module in modulesToRemove)
        {
            RouteModules.Modules.Remove(module);
        }
    }

    public RouteModules RouteModules { get; }
}