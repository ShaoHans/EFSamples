using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._1.属性映射
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                Student stu = new Student
                {
                    Name = "jik",
                    Age = 20,
                    Courses = new List<Course>
                    {
                        new Course{ Name="EF"}
                    },
                    HomeAddress = new Address()
                };
                dbContext.Students.Add(stu);
                dbContext.SaveChanges();
            }
            using (var dbContext = new EfDbContext())
            {
                var stu = dbContext.Students.Include(s => s.Courses).FirstOrDefault();
                stu.Name = "hhah";
                dbContext.SaveChanges();
                foreach (var c in stu.Courses)
                {
                    Console.WriteLine($"{c.Name}");
                }
            }
                Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
