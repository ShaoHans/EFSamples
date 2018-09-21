using _2._2.约定.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._2.约定
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }

        [NonUnicode]
        public string Mobile { get; set; }

        public DateTime? JoinDate { get; set; }

        public int Gender { get; set; }

        public decimal Salary { get; set; }

        // 约定1：类型发现。在EfDbContext中通过DbSet暴露Employee模型时，模型中的派生类将自动被包含，
        // 不需要再设置DbSet<Department>属性
        public virtual Department Department { get; set; }
    }

    public class Department
    {
        
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Company
    {
        //约定2：主键约定。CodeFirst根据模型中定义的ID或者类名加上ID的属性作为主键，如果主键为id或guid类型，
        // 那么主键将被映射成自增长标识列；若没有上述两个属性中的任何一个，则报错：has no key defined
        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public DateTime? BeginDate { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal Amount { get; set; }

        public class Address
        {
            public string Province { get; set; }

            public string City { get; set; }

            public string Country { get; set; }
        }
    }

}
