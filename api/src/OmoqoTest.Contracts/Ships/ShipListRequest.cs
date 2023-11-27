using OmoqoTest.Contracts.Common;

namespace OmoqoTest.Contracts.Ships
{
    public class ShipListRequest : PaginatedList
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        
    }
}