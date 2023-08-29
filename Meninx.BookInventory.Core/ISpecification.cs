namespace Meninx.BookInventory
{
    public interface ISpecification<T>
    {
        string Query { get; }

        int Limit { get; }

        int Offset { get; }

        string SortBy { get; }

        string SortOrder { get; }

        ISpecification<T> ApplyQuery(string query);

        ISpecification<T> ApplyPaging(int limit, int offset);

        ISpecification<T> ApplySorting(string sortBy, string sortOrder);
    }
}