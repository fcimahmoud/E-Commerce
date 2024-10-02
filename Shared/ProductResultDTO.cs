
namespace Shared
{
    public record ProductResultDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandName { get; set; }
        public int TypeName { get; set; }
    }
}
