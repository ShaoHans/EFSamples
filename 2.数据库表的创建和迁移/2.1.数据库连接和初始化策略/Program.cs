using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._1.数据库连接和初始化策略
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Blogs.Add(new Blog
                {
                    Name = "ShaoHans",
                    Url = "https://github.com/ShaoHans/EFSamples",
                    CreatedTime = DateTime.Now
                });
                dbContext.SaveChanges();
            }
            Console.WriteLine("操作结束");
            Console.ReadKey();
        }
    }
}
