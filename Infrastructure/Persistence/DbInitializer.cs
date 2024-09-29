
namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;

        public DbInitializer(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Create DataBase If It doesn't Exist & Applying Any Pending Migrations
                if (_storeContext.Database.GetPendingMigrations().Any())
                    await _storeContext.Database.MigrateAsync();

                // Apply Data Seeding
                if (!_storeContext.ProductTypes.Any())
                {
                    // Read Types From File as String 
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    // Transform into C# objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // Add to DB & Save Changes
                    if (types != null && types.Any())
                    {
                        await _storeContext.ProductTypes.AddRangeAsync(types);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.ProductBrands.Any())
                {
                    // Read Types From File as String 
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    // Transform into C# objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // Add to DB & Save Changes
                    if (brands != null && brands.Any())
                    {
                        await _storeContext.ProductBrands.AddRangeAsync(brands);
                        await _storeContext.SaveChangesAsync();
                    }
                }

                if (!_storeContext.Products.Any())
                {
                    // Read Types From File as String 
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    // Transform into C# objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // Add to DB & Save Changes
                    if (products != null && products.Any())
                    {
                        await _storeContext.Products.AddRangeAsync(products);
                        await _storeContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}