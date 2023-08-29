namespace Meninx.BookInventory
{

    public class Specification<T> : ISpecification<T>
    {
        public string Query { get; private set; }

        public int? Limit { get; private set; }

        public int? Offset { get; private set; }

        public string SortBy { get; private set; }

        public string SortOrder { get; private set; }

        public Specification()
        {

        }

        public ISpecification<T> ApplyQuery(string query)
        {
            Query = query;

            return this;
        }

        public ISpecification<T> ApplyPaging(int? limit, int? offset)
        {
            Limit = limit;
            Offset = offset;

            return this;
        }

        public ISpecification<T> ApplySorting(string sortBy, string sortOrder)
        {
            SortBy = sortBy;
            SortOrder = sortOrder;

            return this;
        }
    }
}