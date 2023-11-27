namespace OmoqoTest.Contracts.Common
{
    public class PaginatedList
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string? OrderBy { get; set; } = null;
    };
}