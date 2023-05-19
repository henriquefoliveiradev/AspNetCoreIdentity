using AspNetCoreIdentity.Data;
using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentity.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Adicionando Autorizações personalizadas por policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));

                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));
            });

            // Politica de cookies (LGPD)
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Adicionando suporte ao contexto do Identity via EF
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));

            // Adicionando configuração padrão do Identity
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

            return services;
        }
    }
}
