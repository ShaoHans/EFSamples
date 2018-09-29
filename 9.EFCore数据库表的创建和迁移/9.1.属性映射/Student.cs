using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._1.属性映射
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int SeqNo { get; set; }
        public decimal Amount { get; set; }
        public Address HomeAddress { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }

    public class Address
    {
        public string Province { get; set; }

        public string City { get; set; }
    }
}
