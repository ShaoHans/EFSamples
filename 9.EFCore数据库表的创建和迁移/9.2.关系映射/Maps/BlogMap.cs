using _9._2.关系映射.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2.关系映射.Maps
{
    public class BlogMap : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog");
            builder.Property(b => b.Name).HasColumnType("varchar(40)").IsRequired();
            builder.Property(b => b.CreateTime).HasDefaultValueSql("getdate()");
            builder.Property(b => b.ModifiedTime).HasDefaultValueSql("getdate()");

            builder.HasMany(b => b.Posts)
                .WithOne(p => p.Blog)
                .HasForeignKey(p => p.BlogId);
        }
    }
}
