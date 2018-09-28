using Infrastructure.NetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._2.结构化日志输出
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.OrderItems.ToList();
                dbContext.Database.ExecuteSqlCommand("select * from orders");
            }
                Console.ReadKey();
        }
    }
}
