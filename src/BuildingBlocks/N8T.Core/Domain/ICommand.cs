using MediatR;

namespace N8T.Core.Domain
{
    public interface ICommand<out T> : IRequest<T>
    {
    }
}
