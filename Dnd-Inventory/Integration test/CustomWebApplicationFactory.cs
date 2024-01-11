using Dnd_Inventory_DAL;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Integration_test
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<SessionDbContext>));

                if(dbContextDescriptor is not null)
                    services.Remove(dbContextDescriptor);

                // database context
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                services.AddDbContext<SessionDbContext>(
                options =>
                        options.UseMySQL(configuration.GetConnectionString("Test"))
                );

                // allow anonymous access to bypass authorization
                var authorizationDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IAuthorizationHandler));
                if (authorizationDescriptor != null)
                    services.Remove(authorizationDescriptor);

            });

            builder.UseEnvironment("Development");

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        }
    }
}
