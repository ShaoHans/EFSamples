using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Maps
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            ToTable("Order");

            HasKey(o => o.Id);

            Property(o => o.OrderNo).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            Property(o => o.TotalAmount).HasPrecision(18, 2).IsRequired();
            Property(o => o.UserName).HasMaxLength(40).IsRequired();

            HasMany(o => o.Items).WithRequired(a => a.Order).HasForeignKey(a => a.OrderId);
        }
    }
}
