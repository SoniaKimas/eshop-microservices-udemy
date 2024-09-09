using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infraestruture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructureServices
        (this IServiceCollection services,
        IConfiguration configuration)
    {

        // TODO: Add services here
        var connectionString = configuration.GetConnectionString("Database");

        // Add DbContext
        // services.AddDbContext
        // add application dbcontext


        return services;
    }
}
