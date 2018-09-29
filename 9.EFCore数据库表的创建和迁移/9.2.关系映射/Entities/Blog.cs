using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2.关系映射.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
