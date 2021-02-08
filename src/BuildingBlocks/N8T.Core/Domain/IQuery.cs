using MediatR;

namespace N8T.Core.Domain
{
    public interface IQuery<T> : IRequest<ResultModel<T>>
    {
    }
}
