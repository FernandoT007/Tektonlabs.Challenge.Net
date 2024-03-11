using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tektonlabs.Challenge.Net.Application.Abstractions.Clock;
using Tektonlabs.Challenge.Net.Application.Abstractions.Data;
using Tektonlabs.Challenge.Net.Application.Discount;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Discount;
using Tektonlabs.Challenge.Net.Domain.Products;
using Tektonlabs.Challenge.Net.Domain.Status;
using Tektonlabs.Challenge.Net.Infrastructure.Clock;
using Tektonlabs.Challenge.Net.Infrastructure.Data;
using Tektonlabs.Challenge.Net.Infrastructure.Repositories;

namespace Tektonlabs.Challenge.Net.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        var discountApiBaseUrl = configuration.GetValue<string>("ExternalServices:ServiceDiscount") ?? throw new ArgumentNullException(nameof(configuration));
        services.AddHttpClient("DiscountHttpClient", client => { client.BaseAddress = new Uri(discountApiBaseUrl); });
        services.AddTransient<IDiscountService, DiscountService>(); 

        var connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention(); });

        services.AddMemoryCache();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(sc => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}