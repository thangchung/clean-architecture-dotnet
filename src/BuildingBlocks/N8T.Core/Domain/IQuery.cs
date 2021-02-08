using MediatR;

namespace N8T.Core.Domain
{
    public interface IQuery<out T> : IRequest<T>
    {
    }
}
