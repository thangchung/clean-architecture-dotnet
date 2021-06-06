using System.Collections.Generic;
using MediatR;

namespace N8T.Core.Domain
{
    public interface ICommand<T> : IRequest<ResultModel<T>>
        where T : notnull
    {
    }

    public interface IQuery<T> : IRequest<ResultModel<T>>
        where T : notnull
    {
    }

    public interface ICreateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
        where TRequest : notnull
        where TResponse : notnull
    {
        public TRequest Model { get; init; }
    }

    public interface IUpdateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
        where TRequest : notnull
        where TResponse : notnull
    {
        public TRequest Model { get; init; }
    }

    public interface IDeleteCommand<TId, TResponse> : ICommand<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        public TId Id { get; init; }
    }

    public interface IListQuery<TResponse> : IQuery<TResponse>
        where TResponse : notnull
    {
        public List<string> Includes { get; init; }
        public List<FilterModel> Filters { get; init; }
        public List<string> Sorts { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public interface IItemQuery<TId, TResponse> : IQuery<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        public List<string> Includes { get; init; }
        public TId Id { get; init; }
    }

    public record FilterModel(string FieldName, string Comparision, string FieldValue);

    public record ResultModel<T>(T Data, bool IsError = false, string ErrorMessage = default) where T : notnull
    {
        public static ResultModel<T>  Create(T data, bool isError = false, string errorMessage = default)
        {
            return new (data, isError, errorMessage);
        }
    }

    public record ListResponseModel<T>(List<T> Items, long TotalItems, int Page, int PageSize)  where T : notnull
    {
        public static ListResponseModel<T> Create(List<T> items, long totalItems = 0, int page = 1, int pageSize = 20)
        {
            return new (items, totalItems, page, pageSize);
        }
    }
}
