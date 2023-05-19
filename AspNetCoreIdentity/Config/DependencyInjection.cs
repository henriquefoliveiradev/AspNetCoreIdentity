using AspNetCoreIdentity.Extensions;
using KissLog;
using KissLog.AspNetCore;
using KissLog.Formatters;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentity.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
            services.AddScoped<AuditoriaFilter>();
            return services;
        }
    }
}
