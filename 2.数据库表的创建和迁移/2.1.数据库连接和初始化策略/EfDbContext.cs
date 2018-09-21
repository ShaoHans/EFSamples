using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._1.数据库连接和初始化策略
{
    public class EfDbContext: DbContext
    {
        public EfDbContext():base("name=EfDbConnString")
        {
            // 数据库初始化策略

            // 若数据库不存在则创建
            //Database.SetInitializer(new CreateDatabaseIfNotExists<EfDbContext>());

            // 无论存在与否，每次都新建数据库
            //Database.SetInitializer(new DropCreateDatabaseAlways<EfDbContext>());

            // EF模型发生变化则先删除再重建数据库
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());

            // 禁用初始化策略
            Database.SetInitializer<EfDbContext>(null);
        }
        public DbSet<Blog> Blogs { get; set; }
    }
}
