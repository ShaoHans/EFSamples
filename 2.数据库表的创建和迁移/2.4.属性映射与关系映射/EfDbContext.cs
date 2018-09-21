using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._4.属性映射与关系映射
{
    public class EfDbContext:DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public EfDbContext() : base("name=EfDbConnString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //设置表名
            modelBuilder.Entity<Blog>().ToTable("Blog");
            modelBuilder.Entity<User>().ToTable("User");
            // 设置字段别名
            modelBuilder.Entity<Blog>().Property(b => b.UserName).HasColumnName("UserAccount");

            //设置主键：名称为Id的属性默认映射为主键且为标识列自增长
            modelBuilder.Entity<Blog>().HasKey(b => b.Id)
                .Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // 设置联合主键
            //modelBuilder.Entity<Blog>().HasKey(b => new { b.Id,b.Id2 });

            // 设置字符串：string类型默认映射为nvarchar(max)
            modelBuilder.Entity<Blog>().Property(b => b.Title).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Blog>().Property(b => b.Code).HasColumnType("char").HasMaxLength(6);

            //  设置为nchar(20)类型，IsUnicode(false)参数设置为false则为char(20)
            modelBuilder.Entity<Blog>().Property(b => b.UserName).HasMaxLength(20).IsFixedLength().IsUnicode(true);
            
            // 日期
            modelBuilder.Entity<Blog>().Property(b => b.CreatedTime).IsOptional();
            modelBuilder.Entity<Blog>().Property(b => b.Timespan).HasColumnType("time").IsOptional();

            // 复杂类型
            modelBuilder.ComplexType<Address>();
            modelBuilder.ComplexType<Address>().Property(a => a.City).HasMaxLength(30);

            //乐观并发属性
            modelBuilder.Entity<Blog>().Property(b => b.Title).IsConcurrencyToken();
            //modelBuilder.Entity<User>().Property(u => u.IdNumber).IsRowVersion();

            //索引
            modelBuilder.Entity<Blog>().HasIndex(b => b.UserName);
        }

    }
}
