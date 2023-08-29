namespace Meninx.BookInventory
{
    public interface ISpecification<T>
    {
        string Query { get; }

        int Limit { get; }

        int Offset { get; }

        string SortBy { get; }

        string SortOrder { get; }
    }
}