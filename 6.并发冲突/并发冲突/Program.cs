using Infrastructure.NetFramework;
using Infrastructure.NetFramework.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 并发冲突
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init();
            //Test();
            //Test2();
            Test3();
            Console.ReadKey();
        }

        static void Init()
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Customers.Add(new Customer
                {
                    Name = "shz",
                    Age = 18,
                    CreateTime = DateTime.Now
                });
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 客户端获胜
        /// </summary>
        static void Test()
        {
            using (EfDbContext dbContext = new EfDbContext())
            using (EfDbContext dbContext2 = new EfDbContext())
            {
                int id = 1;
                var customer = dbContext.Customers.FirstOrDefault(c => c.Id == id);
                var customer2 = dbContext2.Customers.FirstOrDefault(c => c.Id == id);

                customer.Name = "jim";
                dbContext.SaveChanges();

                try
                {
                    customer2.Name = "tom";
                    dbContext2.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();

                    //获取数据库中原始值对象
                    var original = (Customer)entry.OriginalValues.ToObject();
                    //获取更新后数据库最新的值对象
                    var db = (Customer)entry.GetDatabaseValues().ToObject();
                    //获取当前内存中的值对象
                    var current = (Customer)entry.CurrentValues.ToObject();

                    Console.WriteLine($"原始值：{original.Id}-{original.Name}-{original.Age}");
                    Console.WriteLine($"数据库中值：{db.Id}-{db.Name}-{db.Age}");
                    Console.WriteLine($"内存值：{current.Id}-{current.Name}-{current.Age}");

                    //相对于第一次更新Name后，把原始值更新为数据库值
                    entry.OriginalValues.SetValues(db);

                    // 再执行第二次更新Name
                    dbContext2.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 数据库获胜
        /// </summary>
        static void Test2()
        {
            using (EfDbContext dbContext = new EfDbContext())
            using (EfDbContext dbContext2 = new EfDbContext())
            {
                int id = 1;
                var customer = dbContext.Customers.FirstOrDefault(c => c.Id == id);
                var customer2 = dbContext2.Customers.FirstOrDefault(c => c.Id == id);

                customer.Name = "jim";
                dbContext.SaveChanges();

                try
                {
                    customer2.Name = "tom";
                    dbContext2.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    //ex.Entries.Single().Reload();
                    //dbContext2.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 客户端和数据库合并获胜
        /// </summary>
        static void Test3()
        {
            using (EfDbContext dbContext = new EfDbContext())
            using (EfDbContext dbContext2 = new EfDbContext())
            {
                int id = 1;
                var customer = dbContext.Customers.FirstOrDefault(c => c.Id == id);
                var customer2 = dbContext2.Customers.FirstOrDefault(c => c.Id == id);

                customer.Name = "jim2";
                customer.Age = 25;
                dbContext.SaveChanges();

                try
                {
                    customer2.Name = "tom2";
                    customer2.Age = 22;
                    dbContext2.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // 对于设置了并发控制的属性使用客户端获胜模式
                    var entry = ex.Entries.Single();
                    var dbValues = entry.GetDatabaseValues();
                    var orginalValues = entry.OriginalValues.Clone();
                    entry.OriginalValues.SetValues(dbValues);

                    // 没有设置并发控制的属性使用数据库获胜模式
                    dbValues.PropertyNames.Where(p => !object.Equals(orginalValues[p], dbValues[p]))
                        .ToList()
                        .ForEach(p => entry.Property(p).IsModified = false);
                    dbContext2.SaveChanges();
                }
            }
        }
    }
}
