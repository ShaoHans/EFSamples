using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._1.属性映射
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Student Student { get; set; }
    }
}
