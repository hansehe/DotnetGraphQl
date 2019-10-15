using GraphQL;
using GraphQL.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetGraphQl.GraphQl
{
    public static class GraphQlRegistration
    {
        public static IServiceCollection RegisterGraphQl(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IDependencyResolver>(x => new FuncDependencyResolver(x.GetRequiredService))
                .AddScoped<PersonalInfoSchema>()
                .AddGraphQL(x =>
                {
                    var section = configuration.GetSection("graphQl");
                    x.EnableMetrics = section.GetValue<bool>("enableMetrics");
                    x.ExposeExceptions = section.GetValue<bool>("exposeExceptions");
                })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddWebSockets()
                .AddDataLoader();

            return services;
        }
    }
}