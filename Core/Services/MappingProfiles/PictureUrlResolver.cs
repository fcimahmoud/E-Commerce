
namespace Services.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDTO, string>
    {
        public string Resolve(Product source, ProductResultDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
