using Infrastructure.NetCore.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.NetCore
{
    public class EfCoreDbContext: DbContext
    {
        public EfCoreDbContext(DbContextOptions<EfCoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new OrderItemMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
