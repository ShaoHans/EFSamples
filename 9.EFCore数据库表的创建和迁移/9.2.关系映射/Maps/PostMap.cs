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
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");
            builder.Property(p => p.Title).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(b => b.CreateTime).HasDefaultValueSql("getdate()");
            builder.Property(b => b.ModifiedTime).HasDefaultValueSql("getdate()");
        }
    }
}
