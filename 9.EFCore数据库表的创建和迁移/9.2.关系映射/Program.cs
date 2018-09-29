using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2.关系映射
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //var posts = dbContext.Posts.Include(p => p.Tags).ToList(); 获取不到Tags
                var posts = dbContext.Posts.Include("PostTags.Tag").ToList();
            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
