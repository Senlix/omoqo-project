namespace OmoqoTest.Domain.Common
{
    public class PaginatedList<T>(IReadOnlyCollection<T> Items, int Count, int Page, int Limit)
    {
        public IReadOnlyCollection<T> Items { get; set; } = Items;
        public int Page { get; set; } = Page;
        public int Limit { get; set; } = Limit;
        public int Count { get; set; } = Count;
    };
}