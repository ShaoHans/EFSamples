using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._4.属性映射与关系映射
{
    public class User
    {
        public User()
        {
            Address = new Address();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IdNumber { get; set; }

        public Address Address { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}
