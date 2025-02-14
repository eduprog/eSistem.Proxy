using System.Numerics;
using eSistem.Proxy.RouteValidatoreSistem;
using System.Security.Cryptography;
using System.Text;
using eSistem.Proxy;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddLettuceEncrypt();


// Adiciona AppRoutesModulesService ao DI para ser injetado onde necessário
builder.Services.AddSingleton<AppRoutesModulesService>();

builder.ConfigureAppModulesRoutes();

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
var routeService = app.Services.GetRequiredService<AppRoutesModulesService>();

foreach (AppModuleRoute module in routeService.AppModulesRoutes.Modules)
{

    Console.WriteLine($"Módulo: {module.Module.Name} - {module.Module.Description}");
    
    
    foreach (RouteEsistem route in module.Route)
    {
        Console.WriteLine($"  Rota: {route.Path}");
    }
}

app.UseAppModulesRoutes();

app.Run();