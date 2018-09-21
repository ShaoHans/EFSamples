using _2._2.约定.Attributes;
using _2._2.约定.Configurations;
using _2._2.约定.Conventions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _2._2.约定.Order;

namespace _2._2.约定
{
    [DbConfigurationType(typeof(MyDbConfiguration))]
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("name=EfDbConnString")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EfDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 3.复杂类型约定
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.ComplexType<Address>();

            // 全局约定：类型为decimal的字段保留两位小数
            modelBuilder.Properties<decimal>().Configure(cfg => {
                cfg.HasPrecision(10, 2);
            });
            modelBuilder.Properties<string>()
                .Where(p => p.Name == "Name")
                .Configure(c => c.HasMaxLength(40));
            // 自定义约定
            modelBuilder.Conventions.Add<DateTime2Convention>();
            // 特性约定：string类型的属性映射为数据库字段为varchar类型而非nvarchar类型
            modelBuilder.Properties()
                .Where(p => p.GetCustomAttributes(false).OfType<NonUnicode>().Any())
                .Configure(c =>
                {
                    c.IsUnicode(false).HasMaxLength(20);
                });

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
