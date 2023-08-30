using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // including inside this table
            builder.OwnsOne(
                o => o.ShipToAddress,
                a => a.WithOwner()
            );
            // converting `enum int` type to string
            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus) Enum.Parse(typeof(OrderStatus),o)
                );
            
            // if order deleted => orderItems also gone
            // enas ugu breli iwkuma do inas arti atewkuma
            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
            
        }
    }
}