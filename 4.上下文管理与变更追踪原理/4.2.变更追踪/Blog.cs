using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2.变更追踪
{
    public class Blog
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}
