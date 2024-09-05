using MediatR;
using TwEInvoice.Domain.Abstractions;

namespace TwEInvoice.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}