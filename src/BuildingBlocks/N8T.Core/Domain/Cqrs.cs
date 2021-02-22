using System.Collections.Generic;
using MediatR;

namespace N8T.Core.Domain
{
    public interface ICommand<T> : IRequest<ResultModel<T>>
    {
    }

    public interface IQuery<T> : IRequest<ResultModel<T>>
    {
    }

    public interface ICreateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
    {
        public TRequest Model { get; init; }
    }

    public interface IUpdateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
    {
        public TRequest Model { get; init; }
    }

    public interface IDeleteCommand<TId, TResponse> : ICommand<TResponse> where TId : struct
    {
        public TId Id { get; init; }
    }

    public interface IListQuery<TResponse> : IQuery<TResponse>
    {
        public List<string> Includes { get; init; }
        public List<FilterModel> Filters { get; init; }
        public List<string> Sorts { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public interface IItemQuery<TId, TResponse> : IQuery<TResponse>
    {
        public List<string> Includes { get; init; }
        public TId Id { get; init; }
    }

    public record FilterModel(string FieldName, string Comparision, string FieldValue);

    public record ResultModel<T>(T Data, bool IsError = false, string? ErrorMessage = default);

    public record ListResponseModel<T>(List<T> Items, long TotalItems, int Page, int PageSize);
}
