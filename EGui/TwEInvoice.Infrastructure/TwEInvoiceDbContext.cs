using MediatR;
using Microsoft.EntityFrameworkCore;
using TwEInvoice.Application.Exceptions;
using TwEInvoice.Domain.Abstractions;
using TwEInvoice.Domain.Invoices;
using TwEInvoice.Domain.Invoices.InvoiceNumbers;

namespace TwEInvoice.Infrastructure;

public class TwEInvoiceDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public TwEInvoiceDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    internal TwEInvoiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TwInvoice> TwInvoices => Set<TwInvoice>();
    public DbSet<InvoiceBook> InvoiceBooks => Set<InvoiceBook>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TwEInvoiceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            // TODO: Domain events might fail, but data is already persisted (next chapter will introduce outbox pattern)
            await PublishingEventsAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private async Task PublishingEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity).ToList()
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();
                // Seems like there is a potential risk here 
                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}