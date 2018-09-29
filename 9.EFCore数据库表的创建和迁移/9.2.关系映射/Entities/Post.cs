using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2.关系映射.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        private ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        [NotMapped]
        public IEnumerable<Tag> Tags => PostTags.Select(t => t.Tag);
    }
}
