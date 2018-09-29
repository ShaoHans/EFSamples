using Infrastructure.NetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.NetCore.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.Property(o => o.OrderNo).HasColumnType("varchar(20)").IsRequired();
            builder.Property(o => o.CreateTime).HasDefaultValueSql("getdate()");
        }
    }
}
