using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1.上下文非线程安全
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
            Console.ReadKey();
        }

        static async void Test()
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Blogs.Add(new Blog
                {
                    Title = "blog",
                    CreateTime = DateTime.Now
                });

                // 如下用法会报异常：异步操作只支持同一时刻操作一次
                //dbContext.SaveChangesAsync();
                //dbContext.Blogs.ToListAsync();

                await dbContext.SaveChangesAsync();
                var list = await dbContext.Blogs.ToListAsync();
                foreach (var blog in list)
                {
                    Console.WriteLine($"{blog.Title}创建于{blog.CreateTime}");
                }
            }
        }
    }
}
