using Infrastructure.NetFramework.Entities;
using Infrastructure.NetFramework.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework
{
    [DbConfigurationType(typeof(MyDbConfiguration))]
    public class EfDbContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<SqlLog> SqlLogs { get; set; }

        public EfDbContext() : base("name=EfDbConnString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var mapsToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !string.IsNullOrWhiteSpace(t.Namespace)
                && t.BaseType != null
                && t.BaseType.IsGenericType
                && t.BaseType.GetGenericTypeDefinition() == (typeof(EntityTypeConfiguration<>)));
            foreach (var map in mapsToRegister)
            {
                dynamic mapInstance = Activator.CreateInstance(map);
                modelBuilder.Configurations.Add(mapInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
