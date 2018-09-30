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

            // 初始化数据
            builder.HasData(
                new Order
                {
                    Id = 1,
                    OrderNo = "PM1111",
                    TotalAmount = 100M,
                    UserName = "jim",
                    CreateTime = DateTime.Now
                },
                new Order
                {
                    Id = 2,
                    OrderNo = "PM2222",
                    TotalAmount = 79M,
                    UserName = "tom",
                    CreateTime = DateTime.Now
                },
                new Order
                {
                    Id = 3,
                    OrderNo = "PM3333",
                    TotalAmount = 230M,
                    UserName = "lucy",
                    CreateTime = DateTime.Now
                }
                );
        }
    }
}
