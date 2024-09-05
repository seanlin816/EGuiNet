using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace TwEInvoice.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.NotificationPublisher = new ForeachAwaitPublisher();
            // configuration.AddOpenBehavior(typeof(CommandLoggingBehavior<,>)); // TODO: What is this, what about other behaviors?
        });
        return services;
    }
}