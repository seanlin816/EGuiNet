using MediatR;
using TwEInvoice.Domain.Abstractions;

namespace TwEInvoice.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}