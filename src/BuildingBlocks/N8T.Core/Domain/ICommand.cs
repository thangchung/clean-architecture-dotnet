using MediatR;

namespace N8T.Core.Domain
{
    public interface ICommand<T> : IRequest<ResultModel<T>>
    {
    }
}
