using _7._1.预编译视图.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._1.预编译视图
{
    [DbConfigurationType(typeof(MyDbConfiguration))]
    public class EfDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public EfDbContext() : base("name=EfDbConnString")
        {
            
        }

    }
}
