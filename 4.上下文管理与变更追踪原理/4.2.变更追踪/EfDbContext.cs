using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2.变更追踪
{
    public class EfDbContext: DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public EfDbContext() : base("name=EfDbConnString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
            Configuration.AutoDetectChangesEnabled = false;// 关闭快照式变更追踪
        }
    }
}
