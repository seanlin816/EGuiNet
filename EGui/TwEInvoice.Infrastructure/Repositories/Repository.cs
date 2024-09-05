using Microsoft.EntityFrameworkCore;
using TwEInvoice.Domain.Abstractions;

namespace TwEInvoice.Infrastructure.Repositories;

internal abstract class Repository<T> 
    where T: Entity
{
    protected readonly TwEInvoiceDbContext DbContext;

    protected Repository(TwEInvoiceDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Ulid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public virtual async Task<IReadOnlyList<T>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>().ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual void Add(T entity)
    {
        DbContext.Set<T>().Add(entity);
    }
    
}