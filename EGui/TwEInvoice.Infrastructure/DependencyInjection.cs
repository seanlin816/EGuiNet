using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;
using TwEInvoice.Infrastructure.Repositories;

namespace TwEInvoice.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        AddPersistence(services, configuration); 
        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<TwEInvoiceDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TwEInvoiceDbContext>());
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceBookRepository, InvoiceBookRepository>();
        // services.AddScoped<IDatabaseService, DatabaseService>();
    }

}