using eSistem.Proxy.RouteValidatoreSistem;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddLettuceEncrypt();


// Adiciona RouteSettingService ao DI para ser injetado onde necessário
builder.Services.AddSingleton<RouteSettingService>();

var app = builder.Build();
//  app.Use(async (context, next) =>
//  {
//      //preciso que quando caia no default que ele redirecione para outro site, como faço isso?
//
//
//      switch (context.Request.Path)
//      {
//          case "/identityrecords":
//              context.Response.Redirect("https://api.esistem.com.br:5100/swagger");
//              break;
//          case "/records":
//              context.Response.Redirect("https://api.esistem.com.br:5120/swagger");
//              break;
//          case "/basicrecords":
//              context.Response.Redirect("https://api.esistem.com.br:5110/swagger");
//              break;
//          default:
//              context.Response.Redirect("https://api.esistem.com.br");
//              return;
//      }
//      await next();
// });


app.MapReverseProxy();



// Obtém o serviço e imprime as rotas carregadas
var routeService = app.Services.GetRequiredService<RouteSettingService>();

foreach (AppModuleRoute module in routeService.RouteModules.Modules)
{

    Console.WriteLine($"Módulo: {module.Module.Name} - {module.Module.Description}");
    
    
    foreach (RouteEsistem route in module.Route)
    {
        Console.WriteLine($"  Rota: {route.Path}");
    }
}
app.Run();