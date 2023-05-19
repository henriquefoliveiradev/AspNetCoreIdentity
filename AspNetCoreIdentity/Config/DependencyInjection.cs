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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IKLogger>((provider) => Logger.Factory.Get());
            services.AddLogging(provider =>
            {
                provider
                    .AddKissLog(options =>
                    {
                        options.Formatter = (FormatterArgs args) =>
                        {
                            if (args.Exception == null)
                                return args.DefaultValue;

                            string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                            return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
                        };
                    });
            });


            return services;
        }
    }
}
