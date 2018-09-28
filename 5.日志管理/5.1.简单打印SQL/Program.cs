using Infrastructure.NetFramework;
using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._1.简单打印SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init();
            Test();
            Test2();
            Console.ReadKey();
        }

        static void Init()
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                // 通过接受委托把SQL语句打印到控制台
                dbContext.Database.Log = Console.WriteLine;

                Order order = new Order
                {
                    OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    TotalAmount = 100,
                    CreateTime = DateTime.Now,
                    UserName = "买家1",
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductName = "苹果",
                            Qty = 10,
                            Price =1.02M,
                            CreateTime = DateTime.Now
                        },
                        new OrderItem
                        {
                            ProductName = "香蕉",
                            Qty = 4,
                            Price =1.34M,
                            CreateTime = DateTime.Now
                        },
                    }
                };

                dbContext.Orders.Add(order);
                dbContext.SaveChanges();

            }
        }

        static void Test()
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                var orders = dbContext.Orders.Where(o => o.UserName == "买家1");
                // 通过ToString()方法直接生产sql语句
                var sql = orders.ToString();
                Console.WriteLine(sql);
            }
        }

        static void Test2()
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sql.log");
                StreamWriter sw = new StreamWriter(path) {AutoFlush = true };
                dbContext.Database.Log = (sql => { sw.WriteLine(sql); });
                var order = dbContext.Orders.ToList();
            }
        }
    }
}
