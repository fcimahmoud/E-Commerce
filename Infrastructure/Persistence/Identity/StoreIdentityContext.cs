
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using UserAddress = Domain.Entities.Identity.Address;
using Domain.Entities.Identity;

namespace Persistence.Identity
{
    public class StoreIdentityContext : IdentityDbContext<User>
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAddress>().ToTable("Addresses");
        }
    }
}
