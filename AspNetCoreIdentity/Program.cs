using AspNetCoreIdentity.Config;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;

var builder = WebApplication.CreateBuilder(args);

//Configurando para definir o appsettings automaticamente
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// *** Configurando serviços no container ***
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ResolveDependencies();

// Adicionando MVC no pipeline
builder.Services.AddControllersWithViews();

// Adicionando suporte a componentes Razor (ex. Telas do Identity)
builder.Services.AddRazorPages();

var app = builder.Build();

// *** Configurando o resquest dos serviços no pipeline *** 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

// Redirecionamento para HTTPs
app.UseHttpsRedirection();

// Uso de arquivos estáticos (ex. CSS, JS)
app.UseStaticFiles();

// Adicionando suporte a rota
app.UseRouting();

// Autenticacao e autorização (Identity)
app.UseAuthentication();
app.UseAuthorization();

// Rota padrão
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Mapeando componentes Razor Pages (ex: Identity)
app.MapRazorPages();

app.UseKissLogMiddleware(options => {
    options.Listeners.Add(new RequestLogsApiListener(new Application(
        builder.Configuration["KissLog.OrganizationId"],
        builder.Configuration["KissLog.ApplicationId"])
        )
    {
        ApiUrl = builder.Configuration["KissLog.ApiUrl"]
    });
});

app.Run();