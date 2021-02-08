#nullable enable
using System.Collections.Generic;

namespace N8T.Core.Domain
{
    public interface ICrudInput
    {
    }

    public interface ICreateInput<T> : ICrudInput
    {
        public T Model { get; init; }
    }

    public interface IUpdateInput<T> : ICrudInput
    {
        public T Model { get; init; }
    }

    public interface IDeleteInput<TId> : ICrudInput where TId : struct
    {
        public TId Id { get; init; }
    }

    public interface IListQueryInput : ICrudInput
    {
        public List<string> Includes { get; init; }
        public List<FilterModel> Filters { get; init; }
        public List<string> Sorts { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public interface IItemQueryInput<TId> : ICrudInput where TId : struct
    {
        public List<string> Includes { get; init; }
        public TId Id { get; init; }
    }

    public record FilterModel(string FieldName, string Comparision, string FieldValue);

    public record ResultModel<T>(T Data, bool IsError = false, string? ErrorMessage = default);
}
