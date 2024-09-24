using BookStore.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.OrderStatus).HasConversion(
                (OStatus) => OStatus.ToString(),
                (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );
            builder.Property(o => o.SubTotal).HasColumnType("decimal (18,2)");
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.OwnsOne(o => o.OrderAddress, OrderAddress => OrderAddress.WithOwner());
        }
    }
}
