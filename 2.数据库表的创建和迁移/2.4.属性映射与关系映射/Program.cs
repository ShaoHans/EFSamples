using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._4.属性映射与关系映射
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                dbContext.Blogs.Add(new Blog
                {
                    Title = "博客1",
                    Timespan = TimeSpan.FromSeconds(10)
                });

                dbContext.Users.Add(new User
                {
                    Name = "jim",
                    IdNumber= "323423423423",
                    Address = new Address
                    {
                        City = "ANi",
                        Street = "sdfsdf",
                        ZipCode = "232332"
                    }
                });

                dbContext.SaveChanges();
            }

            using (EfDbContext dbContext = new EfDbContext())
            {
                foreach (var user in dbContext.Users)
                {
                    if(user.Address==null)
                    {
                        Console.WriteLine("没有地址");
                    }
                    else
                    {
                        Console.WriteLine($"{user.Address.City}");
                    }
                }
            }

                Console.WriteLine("操作完成");
            Console.ReadKey();
        }
    }
}
