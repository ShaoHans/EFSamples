using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._1.属性映射
{
    public class EfDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["EfDbConnString"].ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 全局约定
            /*
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.Name).ToTable($"T_{entity.ClrType.Name}");

                foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(string)))
                {
                    property.SetMaxLength(40);
                }
            }
            */

            modelBuilder.Entity<Student>(s =>
            {
                s.ToTable("Student");
                s.HasKey(k => k.Id);
                //s.Property(k => k.SeqNo).ValueGeneratedOnAdd();一个表只能有一个自增列

                // 添加或修改实体时自动使用数据库函数赋值
                s.Property(k => k.ModifyTime).HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
                // 使用自定义的时间生成器
                s.Property(k => k.CreateTime).HasValueGenerator((d, g) => new CreateTimeValueGenerator());

                // 自定义数据库类型
                s.Property(k => k.Amount).HasColumnType("decimal(18,4)");
                s.Property(k => k.Name).HasColumnType("varchar(40)");

                // 添加索引
                s.HasIndex(k => k.Name);
                s.HasIndex(k => new { k.Name, k.Age }).IsUnique();

                // 除主键之外的唯一约束键
                //s.HasAlternateKey(k => k.Name);

                //Owned Entities
                s.OwnsOne(k => k.HomeAddress);
                //.ToTable("StudentHomeAddress");//单独生成一张表
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
