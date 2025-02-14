namespace eSistem.Proxy;

using eSistem.Proxy.RouteValidatoreSistem;

internal static class Startup
{
    internal static WebApplicationBuilder ConfigureAppModulesRoutes(
        this WebApplicationBuilder builder
    )
    {
        // Adiciona AppRoutesModulesService ao DI para ser injetado onde necessário
        builder.Services.AddSingleton<AppRoutesModulesService>();
        return builder;
    }


    internal static WebApplication UseAppModulesRoutes(this WebApplication app)
    {
        app.UseMiddleware<AppModulesRoutesMiddleware>();
        return app;
    }
}