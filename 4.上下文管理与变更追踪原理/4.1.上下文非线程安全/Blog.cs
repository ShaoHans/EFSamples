using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1.上下文非线程安全
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
