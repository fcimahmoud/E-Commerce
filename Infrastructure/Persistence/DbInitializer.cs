
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer (
        StoreContext storeContext, 
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager,
        StoreIdentityContext storeIdentityContext
        )
        : IDbInitializer
    {

        public async Task InitializeAsync()
        {
            try
            {
                // Create DataBase If It doesn't Exist & Applying Any Pending Migrations
                if (storeContext.Database.GetPendingMigrations().Any())
                    await storeContext.Database.MigrateAsync();

                // Apply Data Seeding
                if (!storeContext.ProductTypes.Any())
                {
                    // Read Types From File as String 
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                    // Transform into C# objects
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // Add to DB & Save Changes
                    if (types != null && types.Any())
                    {
                        await storeContext.ProductTypes.AddRangeAsync(types);
                        await storeContext.SaveChangesAsync();
                    }
                }

                if (!storeContext.ProductBrands.Any())
                {
                    // Read Types From File as String 
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                    // Transform into C# objects
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // Add to DB & Save Changes
                    if (brands != null && brands.Any())
                    {
                        await storeContext.ProductBrands.AddRangeAsync(brands);
                        await storeContext.SaveChangesAsync();
                    }
                }

                if (!storeContext.Products.Any())
                {
                    // Read Types From File as String 
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                    // Transform into C# objects
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // Add to DB & Save Changes
                    if (products != null && products.Any())
                    {
                        await storeContext.Products.AddRangeAsync(products);
                        await storeContext.SaveChangesAsync();
                    }
                }
                if (!storeContext.DeliveryMethods.Any())
                {
                    // Read Types From File as String 
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\delivery.json");

                    // Transform into C# objects
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(data);

                    // Add to DB & Save Changes
                    if (methods != null && methods.Any())
                    {
                        await storeContext.DeliveryMethods.AddRangeAsync(methods);
                        await storeContext.SaveChangesAsync();
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
            // Create DataBase If It doesn't Exist & Applying Any Pending Migrations
            if (storeIdentityContext.Database.GetPendingMigrations().Any())
                await storeIdentityContext.Database.MigrateAsync();

            // Seed Default Roles
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Seed Default Users
            if(!userManager.Users.Any())
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

                await userManager.CreateAsync(superAdminUser, "Passw0rd");
                await userManager.CreateAsync(adminUser, "Passw0rd");

                await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}