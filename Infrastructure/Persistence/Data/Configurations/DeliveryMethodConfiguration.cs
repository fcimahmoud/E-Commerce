﻿
namespace Persistence.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,3)");
        }
    }
}
