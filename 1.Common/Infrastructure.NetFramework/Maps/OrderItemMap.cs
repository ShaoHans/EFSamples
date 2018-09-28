using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Maps
{
    public class OrderItemMap: EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            ToTable("OrderItem");

            HasKey(o => o.Id);

            Property(o => o.ProductName).HasMaxLength(20).IsRequired();
            Property(o => o.Qty).IsRequired();
            Property(o => o.Price).HasPrecision(18, 2);
        }
    }
}
