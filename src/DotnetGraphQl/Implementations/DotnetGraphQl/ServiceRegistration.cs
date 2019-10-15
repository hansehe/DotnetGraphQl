using DotnetGraphQl.Abstractions;
using DotnetGraphQl.GraphQl;
using DotnetGraphQl.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetGraphQl
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterService(this IServiceCollection serviceCollection, 
            IConfiguration configuration)
        {
            return serviceCollection
                .AddSingleton(sp => configuration)
                .RegisterLogging()
                .RegisterHealthChecks()
                .AddHttpClient()
                .RegisterGraphQl(configuration)
                .AddScoped<IPersonalInfoHandler, PersonalInfoHandler>();
        }

        private static IServiceCollection RegisterHealthChecks(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHealthChecks();
            return serviceCollection;
        }
    }
}