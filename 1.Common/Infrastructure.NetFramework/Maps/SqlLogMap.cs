using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Maps
{
    public class SqlLogMap : EntityTypeConfiguration<SqlLog>
    {
        public SqlLogMap()
        {
            ToTable("SqlLog");

            HasKey(s => s.Id);

            Property(s => s.Sql).IsRequired().HasColumnType("varchar");

        }
    }
}
