using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.NetCore
{
    /// <summary>
    /// 该工厂类仅在使用迁移命令时用到
    /// </summary>
    public class EfCoreDbContextFactory : IDesignTimeDbContextFactory<EfCoreDbContext>
    {
        public EfCoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EfCoreDbContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=EfCoreDb;Integrated Security=true;");
            return new EfCoreDbContext(builder.Options);
        }
    }
}
