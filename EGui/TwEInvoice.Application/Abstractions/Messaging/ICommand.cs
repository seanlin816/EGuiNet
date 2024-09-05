using Innorhino.Application.Abstractions.Messaging;
using MediatR;
using TwEInvoice.Domain.Abstractions;

namespace TwEInvoice.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}