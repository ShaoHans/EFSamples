using Infrastructure.NetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.NetCore.Maps
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.Property(o => o.ProductName).HasColumnType("varchar(100)").IsRequired();

            builder.HasData(
                        new OrderItem
                        {
                            Id = 1,
                            ProductName = "苹果",
                            Qty = 12,
                            Price = 1.2M,
                            OrderId = 1,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            Id = 2,
                            ProductName = "香蕉",
                            Qty = 23,
                            Price = 2.3M,
                            OrderId = 1,
                            CreateTime = DateTime.Now
                        }, new OrderItem
                        {
                            Id = 3,
                            ProductName = "凤梨",
                            Qty = 7,
                            Price = 6.2M,
                            OrderId = 2,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            Id = 4,
                            ProductName = "橘子",
                            Qty = 14,
                            Price = 3.4M,
                            OrderId = 2,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            Id = 5,
                            ProductName = "火龙果",
                            Qty = 3,
                            Price = 12.9M,
                            OrderId = 2,
                            CreateTime = DateTime.Now
                        }, new OrderItem
                        {
                            Id = 6,
                            ProductName = "披萨",
                            Qty = 2,
                            Price = 120M,
                            OrderId = 3,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            Id = 7,
                            ProductName = "KFC全家桶",
                            Qty = 1,
                            Price = 118M,
                            OrderId = 3,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            Id = 8,
                            ProductName = "香蕉",
                            Qty = 1,
                            Price = 3.8M,
                            OrderId = 3,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            Id = 9,
                            ProductName = "百事可乐",
                            Qty = 10,
                            Price = 38M,
                            OrderId = 3,
                            CreateTime = DateTime.Now
                        }
                );
        }
    }
}
