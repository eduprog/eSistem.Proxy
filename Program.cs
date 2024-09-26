var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddLettuceEncrypt();
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
app.Run();