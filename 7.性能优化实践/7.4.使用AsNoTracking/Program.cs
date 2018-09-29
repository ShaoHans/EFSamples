using Infrastructure.NetFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._4.使用AsNoTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EfDbContext dbContext = new EfDbContext())
            {
                var customer = dbContext.Customers.AsNoTracking().FirstOrDefault();
                customer.Name = "Lucy";
                var effectRow = dbContext.SaveChanges();
                Console.WriteLine(effectRow);
            }

            Console.ReadKey();
        }
    }
}
