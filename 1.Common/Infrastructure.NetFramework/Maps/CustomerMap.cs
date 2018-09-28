using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Maps
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customer");
            HasKey(c => c.Id);

            // 添加并发token
            Property(c => c.Name).HasMaxLength(40).IsConcurrencyToken();
        }
    }
}
