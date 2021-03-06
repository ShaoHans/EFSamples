﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2.变更追踪
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                //dbContext.Database.Log = Console.WriteLine;
                dbContext.Blogs.Add(new Blog { Title = "测试", CreateTime = DateTime.Now });
                dbContext.SaveChanges();
                var blog = dbContext.Set<Blog>().FirstOrDefault(b => b.Title == "测试");
                Console.WriteLine(dbContext.Entry(blog).State);
                blog.Title = "测试2";
                Console.WriteLine(dbContext.Entry(blog).State);

                // IQueryable进行ToString()查看sql
                var blogs = dbContext.Blogs;
                var sql = blogs.ToString();
                Console.WriteLine(sql);
            }
            Console.ReadKey();
        }
    }
}
