using Infrastructure.NetFramework;
using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._9.关闭回调DetectChanges方法
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                //dbContext.Database.Log = Console.WriteLine;
                List<Customer> customers = new List<Customer>();
                for (int i = 0; i < 2000; i++)
                {
                    customers.Add(new Customer
                    {
                        Name = $"aaa{i+1}",
                        Age = i,
                        CreateTime = DateTime.Now
                    });
                }

                bool f = dbContext.Configuration.AutoDetectChangesEnabled;
                try
                {
                    dbContext.Configuration.AutoDetectChangesEnabled = false;
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    dbContext.Customers.AddRange(customers);
                    dbContext.SaveChanges();
                    sw.Stop();
                    Console.WriteLine($"总耗时：{sw.ElapsedMilliseconds}");
                }
                catch (Exception)
                {
                    
                }
                finally
                {
                    dbContext.Configuration.AutoDetectChangesEnabled = f;
                }
            }
            Console.ReadKey();
        }
    }
}
