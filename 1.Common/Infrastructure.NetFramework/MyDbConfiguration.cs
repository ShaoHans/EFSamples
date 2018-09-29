using Infrastructure.NetFramework.Interceptors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework
{
    public class MyDbConfiguration: DbConfiguration
    {
        public MyDbConfiguration()
        {
            //SetDatabaseInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
            //SetDatabaseInitializer<EfDbContext>(null);
            SetDatabaseInitializer<EfDbContext>(new NullDatabaseInitializer<EfDbContext>());
            DbInterception.Add(new NLogCommandInterceptor());
            DbInterception.Add(new PerformanceMonitoringInterceptor(10));
        }
    }
}
