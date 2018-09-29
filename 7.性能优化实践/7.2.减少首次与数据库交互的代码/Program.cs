using Infrastructure.NetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._2.减少首次与数据库交互的代码
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                // 应用程序第一次启动运行，EF会查询数据库架构是否发生变化等，可以通过在DbConfiguraion派生类中
                // 设置代码 SetDatabaseInitializer<EfDbContext>(null);
                dbContext.Database.Log = Console.WriteLine;
                dbContext.Orders.ToList();
                dbContext.OrderItems.ToList();

                // 单连接多请求支持
                // 配置文件的连接字符串添加 MultipleActiveResultSets=True 属性即可
            }

            Console.WriteLine("============================");

            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                dbContext.Orders.ToList();
            }
            Console.ReadKey();
        }
    }
}
