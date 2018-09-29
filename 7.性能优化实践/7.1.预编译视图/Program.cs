using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._1.预编译视图
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                // 将此代码放在应用程序启动方法中，保证启动时预先编译生成映射试图（EF6.0+）
                var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;
                var mapppingCollection = (StorageMappingItemCollection)objectContext
                    .MetadataWorkspace
                    .GetItemCollection(DataSpace.CSSpace);
            }

                Console.ReadKey();
        }
    }
}
