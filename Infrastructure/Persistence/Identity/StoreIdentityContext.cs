
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
