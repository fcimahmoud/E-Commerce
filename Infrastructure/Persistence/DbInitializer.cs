
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreContext _storeContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreContext storeContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _storeContext = storeContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task InitializeIdentityAsync()
        {
            // Seed Default Roles
            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Seed Default Users
            if(!_userManager.Users.Any())
            {
                var superAdminUser = new User
                {
                    DisplayName = "Super Admin User",
                    Email = "superAdminUser@gmail.com",
                    UserName = "SuperAdminUser",
                    PhoneNumber = "1234567890",
                };
                var adminUser = new User
                {
                    DisplayName = "Admin User",
                    Email = "AdminUser@gmail.com",
                    UserName = "AdminUser",
                    PhoneNumber = "1234567890",
                };

                await _userManager.CreateAsync(superAdminUser, "Passw0rd");
                await _userManager.CreateAsync(adminUser, "Passw0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}