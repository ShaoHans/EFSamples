using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._4.属性映射与关系映射
{
    public class Blog
    {
        public int Id { get; set; }

        public int Id2 { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? CreatedTime { get; set; }

        public string UserName { get; set; }

        public string CoverUrl { get; set; }

        public string Code { get; set; }

        public double Double { get; set; }

        public TimeSpan Timespan { get; set; }
    }
}
