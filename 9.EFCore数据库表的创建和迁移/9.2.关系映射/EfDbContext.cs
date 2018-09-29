using _9._2.关系映射.Entities;
using _9._2.关系映射.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _9._2.关系映射
{
    public class EfDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["EfDbConnString"].ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var maps = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(t => t.BaseType.IsGenericType
            //    && t.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            //foreach (var map in maps)
            //{
            //    dynamic instance = Activator.CreateInstance(map);
            //    modelBuilder.ApplyConfiguration(instance);
            //}

            modelBuilder.ApplyConfiguration(new BlogMap());
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new TagMap());
            modelBuilder.ApplyConfiguration(new PostTagMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
