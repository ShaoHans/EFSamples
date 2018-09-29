using Infrastructure.NetFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._7.避免N加1Select查询
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                //var orders = dbContext.Orders.AsNoTracking().ToList(); 有N+1问题
                var orders = dbContext.Orders.AsNoTracking().Include(o => o.Items).ToList();
                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.Items.Count}");
                }
            }
            Console.ReadKey();
        }
    }
}
