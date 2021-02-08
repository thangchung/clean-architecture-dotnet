#nullable enable
using System.Collections.Generic;

namespace N8T.Core.Domain
{
    public interface ICrudModel
    {
    }

    public interface ICreateModel<T> : ICrudModel
    {
        public T Model { get; init; }
    }

    public interface IUpdateModel<T> : ICrudModel
    {
        public T Model { get; init; }
    }

    public interface IDeleteModel<TId> : ICrudModel where TId : struct
    {
        public TId Id { get; init; }
    }

    public interface IListQueryModel : ICrudModel
    {
        public List<string> Includes { get; init; }
        public List<FilterModel> Filters { get; init; }
        public List<string> Sorts { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public interface IItemQueryModel<TId> : ICrudModel where TId : struct
    {
        public List<string> Includes { get; init; }
        public TId Id { get; init; }
    }

    public record FilterModel(string FieldName, string Comparision, string FieldValue);

    public record ResultModel<T>(T Data, bool IsError = false, string? ErrorMessage = default);
}
