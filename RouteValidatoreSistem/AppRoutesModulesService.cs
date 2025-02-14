namespace eSistem.Proxy.RouteValidatoreSistem;

public class AppRoutesModulesService
{
    public AppRoutesModulesService(IConfiguration configuration)
    {
        AppModulesRoutes = configuration.GetSection("RouteModules")
                                .Get<RouteModules>()
                            ?? new RouteModules();
        CleanRouteAndNameEmpty();
    }

    private void CleanRouteAndNameEmpty()
    {
        // Criar uma lista para armazenar os módulos que precisam ser removidos
        var modulesToRemove = new List<AppModuleRoute>();

        foreach (var module in AppModulesRoutes.Modules)
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
            AppModulesRoutes.Modules.Remove(module);
        }
    }

    public RouteModules AppModulesRoutes { get; }

    public Dictionary<string, List<string>> GroupedModulesRoutes =>
        AppModulesRoutes.Modules
            .Where(module => !string.IsNullOrWhiteSpace(module.Module.Name)) // Evita nomes vazios
            .ToDictionary(
                module => module.Module.Name, // Chave: Nome do Módulo
                module => module.Route.Select(route => route.Path).ToList() // Valor: Lista de Rotas
            );
    }