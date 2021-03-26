using System;
using System.Threading;
using System.Threading.Tasks;

namespace N8T.Infrastructure.TransactionalOutbox.Dapr
{
    public interface ITransactionalOutboxProcessor
    {
        Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = new());
    }
}