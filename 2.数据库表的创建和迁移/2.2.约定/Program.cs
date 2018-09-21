using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._2.约定
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new EfDbContext())
            {
                dbContext.Employees.Add(new Employee
                {
                    Name = "shz",
                    JoinDate = DateTime.Now
                });
                dbContext.SaveChanges();
            }
            Console.WriteLine("操作结束");
            Console.ReadKey();
        }
    }
}
