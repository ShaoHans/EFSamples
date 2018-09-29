using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2.关系映射.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        private ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        [NotMapped]
        public IEnumerable<Post> Posts => PostTags.Select(p => p.Post);
    }
}
