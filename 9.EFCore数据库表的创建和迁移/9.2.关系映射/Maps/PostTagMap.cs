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
    public class PostTagMap : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.ToTable("PostTag");
            builder.HasKey(p => new { p.PostId, p.TagId });

            builder.HasOne(pt => pt.Post).WithMany("PostTags");
            builder.HasOne(pt => pt.Tag).WithMany("PostTags");
        }
    }
}
