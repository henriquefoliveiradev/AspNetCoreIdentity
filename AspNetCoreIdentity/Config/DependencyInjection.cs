using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentity.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            return services;
        }
    }
}
