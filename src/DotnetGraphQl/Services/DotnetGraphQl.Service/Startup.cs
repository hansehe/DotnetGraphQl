using System.Text;
using System.Threading.Tasks;
using DotnetGraphQl.Config;
using DotnetGraphQl.GraphQl;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DotnetGraphQl.Service
{
    public class Startup
    {
        private ILogger<Startup> StartupLogger;
        
        public static readonly IConfiguration Configuration = ConfigBuilderExtensions.GetConfiguration();
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        services
            .RegisterService(Configuration)
            .AddMvc();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IApplicationLifetime lifetime,
            ILoggerFactory loggerFactory)
        {
            StartupLogger = loggerFactory.CreateLogger<Startup>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StartupLogger.LogInformation("Begin Application_Start");
            lifetime.ApplicationStarted.Register(() => StartupLogger.LogInformation("End Application_Start"));
            lifetime.ApplicationStopping.Register(() => StartupLogger.LogInformation("Start Application_Stop"));
            lifetime.ApplicationStopped.Register(() => StartupLogger.LogInformation("End Application_Stop"));
            
            app.UseHealthChecks("/status/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = HealthCheckResponseWriter
            });

            app.UseWebSockets();
            app.UseGraphQLWebSockets<PersonalInfoSchema>();
            app.UseGraphQL<PersonalInfoSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); //to explore playground navigate http://*DOMAIN*/ui/playground
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions()); // to explore voyager middleware at http://*DOMAIN*/ui/voyager
            app.UseAuthentication();
            app.UseMvc();
        }
        
        private static Task HealthCheckResponseWriter(HttpContext context, HealthReport result)
        {
            var output = $"Overall result: {result.Status}\n--------------------------\n";
            foreach (var item in result.Entries)
            {
                output += $"{item.Key}: {item.Value.Status} => {item.Value.Description}\n";
            }
            return context.Response.WriteAsync(output);
        }
    }
}