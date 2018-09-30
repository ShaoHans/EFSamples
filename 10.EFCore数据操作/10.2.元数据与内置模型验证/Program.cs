using Infrastructure.NetCore;
using Infrastructure.NetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;

namespace _10._2.元数据与内置模型验证
{
    class Program
    {
        static void Main(string[] args)
        {
            //MeteData();
            Validation();
            Console.ReadKey();
        }

        static EfCoreDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<EfCoreDbContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=EfCoreDb;Integrated Security=true;");

            // 启用延迟加载，安装Microsoft.EntityFrameworkCore.Proxies包
            builder.UseLazyLoadingProxies();
            // 若出现客户端查询评估提示，则抛出异常
            builder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));
            return new EfCoreDbContext(builder.Options);
        }

        static void MeteData()
        {
            using (var db = GetDbContext())
            {
                var enType = db.Model.FindEntityType(typeof(Order).FullName);
                Console.WriteLine($"表名：{enType.Relational().TableName}");

                foreach (var p in enType.GetProperties())
                {
                    Console.WriteLine($"字段名：{p.Relational().ColumnName}，字段类型：{p.Relational().ColumnType}");
                }
            }
        }

        static void Validation()
        {
            using (var context = GetDbContext())
            {
                Order order = new Order
                {
                    OrderNo = "PMsdfsdf",
                    TotalAmount = 100M,
                    UserName = "",
                    CreateTime = DateTime.Now
                };
                context.Orders.Add(order);
                var errors = context.ExecuteValidation();
                if (errors.Any())
                {
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"{error.ErrorMessage}");
                    }
                }
                else
                {
                    context.SaveChanges();
                }
            }
        }
    }
}
