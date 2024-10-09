
namespace Shared
{
    public class ProductSpecificationsParameters
    {
        private const int MAXPAGESIZE = 10;
        private const int DEFAULTPAGESIZE = 10;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortOptions? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = DEFAULTPAGESIZE;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = value > 10 ? 10 : value; 
        }
        public string? Search { get; set; }

    }

    public enum ProductSortOptions
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
    }
}
