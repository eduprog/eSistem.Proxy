using Microsoft.AspNetCore.Mvc;

namespace eSistem.Proxy.RouteValidatoreSistem;

public class AppModulesRoutesMiddleware(RequestDelegate next, 
    AppRoutesModulesService service,
    ILogger<AppModulesRoutesMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // 🔹 Se o usuário NÃO está autenticado, simplesmente continua com a requisição
        if (context.User.Identity?.IsAuthenticated != true)
        {
            await next(context);
            return;
        }
        // 🔹 Obtém o valor da claim "tenant_scope" (diretamente da requisição)
        var tenantScopeClaim = context.User.Claims.FirstOrDefault(c => c.Type == "tenant_scope")?.Value;
        if (string.IsNullOrEmpty(tenantScopeClaim))
        {
            logger.LogWarning("Tenant_scope não encontrado no token. Verifique suas credenciais.");

            // 🔸 Resposta 403 com mensagem personalizada
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var problem = new ProblemDetails()
            {
                Status = 401,
                Title = "invalid_token",
                Detail = "Tenant_scope não encontrado no token. Verifique suas credenciais."

            };
            await context.Response.WriteAsJsonAsync(problem);
            return;
        }

        // 🔹 Lista de módulos autorizados para o tenant (vem direto do token JWT)
        var appModules = new HashSet<string>(tenantScopeClaim.Split(','));

        // 🔹 Obtém o path da requisição e verifica se está vinculado a um módulo.
        // Nomalizando , retirando a / início e fim e colocando tudo em minúsculo
        var requestPath = context.Request.Path.ToString().Trim('/').ToLower();
        
        //verificar se a rota em requestPath está contida na lista de rotas configuradas
        // e se o módulo ao qual ela é relativa, está em tenant_scope
        var isAuthorized = service
            .GroupedModulesRoutes
            .Any(module => appModules.Contains(module.Key) 
                           && module.Value.Contains(requestPath));

        if (!isAuthorized)
        {
            logger.LogWarning("Tenant não tem acesso a este módulo. Path:{0}. Verifique suas credenciais",requestPath);

            // 🔸 Resposta 403 com mensagem personalizada
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var problem = new ProblemDetails()
            {
                Status = 403,
                Title = "Forbidden",
                Detail = "Tenant não tem acesso a este módulo. Verifique suas credenciais."

            };
            await context.Response.WriteAsJsonAsync(problem);
            return;
        }

        await next(context);
    }
}