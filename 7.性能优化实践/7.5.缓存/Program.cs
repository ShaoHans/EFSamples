using Infrastructure.NetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _7._5.缓存
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test();
            Test2();
            Console.ReadKey();
        }

        /// <summary>
        /// 实体缓存
        /// </summary>
        static void Test()
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                // 查询条件不同，会查两次数据库，但实体一样
                var customer1 = dbContext.Customers.FirstOrDefault(c => c.Id == 1);
                var customer2 = dbContext.Customers.FirstOrDefault(c => c.Name == "jim2");
                Console.WriteLine(object.ReferenceEquals(customer1, customer2));// True

                Console.WriteLine("==========================================");

                // 根据相同主键值Find数据，只会查询一次数据库，然后从缓存中获取
                var customer3 = dbContext.Customers.Find(2);
                var customer4 = dbContext.Customers.Find(2);
                Console.WriteLine(ReferenceEquals(customer3,customer4));// True
            }
        }

        /// <summary>
        /// 翻译sql查询缓存
        /// </summary>
        static void Test2()
        {
            // EF查询数据分两步：
            // 1.将LINQ表达式树编译成数据库表达式树
            // 2.将数据库表达式树生成SQL语句

            //一、将常量替换为变量来参数化查询
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                int id = 1;
                dbContext.Customers.Where(c => c.Id == id).ToList();
                id = 2;
                dbContext.Customers.Where(c => c.Id == id).ToList();
            }
            Console.WriteLine("==========================================");

            // 二、分页查询，使用QueryableExtensions，需要添加using System.Data.Entity;
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Database.Log = Console.WriteLine;
                int pageNumber = 1, pageSize = 10;
                dbContext.Customers.OrderBy(c => c.Id).Skip(()=> pageNumber).Take(()=> pageSize).ToList();
                pageNumber = 3;
                dbContext.Customers.OrderBy(c => c.Id).Skip(()=> pageNumber).Take(()=> pageSize).ToList();
            }
        }
    }
}
