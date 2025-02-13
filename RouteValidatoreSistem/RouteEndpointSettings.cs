namespace eSistem.Proxy.RouteValidatoreSistem;

//internal class RouteSettingService
//{

//    public RouteEndpointSettings RouteEndpointSettings = new();

//    public RouteSettingService()
//    {
//        //Modulo cadastro
//        var moduloCadastro = new AppModuleEsistem
//        {
//            AppModuleId = "app_cadastro",
//            Description = "Módulo de cadasro do eSistem. Pessoas, Categorias, etc."
//        };
//        var rotasCadastro = new List<AppRouteModuleEsistem>()
//        {
//            new AppRouteModuleEsistem()
//            {
//                Path = "cadastro/categoria/incluir"
//            },

//            new AppRouteModuleEsistem()
//            {
//                Path = "cadastro/categoria/atualizar"
//            }

//        };
//        var configModuloCadastro = new RouteSettingsEsistem(moduloCadastro, rotasCadastro);

//        //Modulo Produtos
//        var moduloCategoriasPessoas = new AppModuleEsistem
//        {
//            AppModuleId = "app_categorias",
//            Description = "Módulo de cadasro do eSistem. Pessoas, Categorias, etc."
//        };

//        var rotasCategoriasPessoas = new List<AppRouteModuleEsistem>()
//        {
//            new AppRouteModuleEsistem()
//            {
//                Path = "cadastro/categoria/incluir"
//            },

//            new AppRouteModuleEsistem()
//            {
//                Path = "cadastro/categoria/atualizar"
//            }
//        };

//        var configCategoriasPessoas = new RouteSettingsEsistem(moduloCategoriasPessoas, rotasCategoriasPessoas);



//        RouteEndpointSettings
//            //Modulo Cadastro
//            .AddModules(configModuloCadastro)
//            //Modulo Categoria
//            .AddModules(configCategoriasPessoas)
//            ;
//    }
//}

//internal class RouteEndpointSettings()
//{
//    public List<RouteSettingsEsistem> RouteEndpointeSistem { get; set; } = [];

//    //public RouteEndpointSettings AddModules(RouteSettingsEsistem routeModules)
//    //{
//    //    RouteEndpointeSistem.Add(routeModules);

//    //    return this;
//    //}
//}