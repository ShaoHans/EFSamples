using Infrastructure.NetCore;
using Infrastructure.NetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace _10._1.数据操作
{
    class Program
    {
        static void Main(string[] args)
        {
            //FindInclued();
            //FirstInclude();
            //AggregateFunction();
            //WhereSearch();
            //GroupByTest();
            //InnerJoin();
            //LeftJoin();
            //Concat();
            //PredicateTest();
            //ThreeLoad();
            //Sql();
            //BatchInsert();
            Like();
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

        /// <summary>
        /// Find方法实现饥饿加载
        /// </summary>
        static void FindInclude()
        {
            using (var dbContext = GetDbContext())
            {
                var order = dbContext.Orders.Find(1);
                if(order.Items == null)
                {
                    Console.WriteLine("Find方法不能进行饥饿加载");
                }
                foreach (var nav in dbContext.Entry(order).Navigations)
                {
                    nav.Load();
                }
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"{item.ProductName}");
                }
            }
        }

        static void FirstInclude()
        {
            using (var dbContext = GetDbContext())
            {
                var order = dbContext.Orders.Include(o=>o.Items).FirstOrDefault();
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"{item.ProductName}");
                }
            }
        }

        /// <summary>
        /// 聚合函数
        /// </summary>
        static void AggregateFunction()
        {
            // efcore2.1已全部实现远程查询，不再是内存查询
            using (var dbContext = GetDbContext())
            {
                Console.WriteLine($"订单总金额：{dbContext.Orders.Sum(o => o.TotalAmount)}");
                Console.WriteLine($"最大订单金额：{dbContext.Orders.Max(o => o.TotalAmount)}");
                Console.WriteLine($"订单平均金额：{dbContext.Orders.Average(o => o.TotalAmount)}");
            }
        }

        static void WhereSearch()
        {
            using (var dbContext = GetDbContext())
            {
                // OfType和Cast目前还不支持转换原始类型
                //var list = dbContext.Orders
                //    .Select(o => o.TotalAmount)
                //    .OfType<string>();
                //foreach (var amount in list)
                //{
                //    Console.WriteLine(amount);
                //}


                var orderNos = dbContext.Orders
                    .Select(o => "订单号：" + o.OrderNo);
                /*
                 * 翻译成的SQL语句如下
                 SELECT '订单号：' + [o].[OrderNo]
                  FROM [Order] AS [o]
                  */
                foreach (var orderNo in orderNos)
                {
                    Console.WriteLine(orderNo);
                }

                var defaultOrder = new Order { OrderNo = "无效订单" };
                var order = dbContext.Orders.Where(o => o.Id == 0).DefaultIfEmpty(defaultOrder).FirstOrDefault();
                Console.WriteLine(order.OrderNo);
            }
        }

        /// <summary>
        /// efcore2.1已支持远程执行groupby
        /// </summary>
        static void GroupByTest()
        {
            using (var dbContext = GetDbContext())
            {
                var g = dbContext.OrderItems
                    .GroupBy(o => o.OrderId)
                    .Where(o => o.Count() > 2)
                    .Select(o => new { o.Key, Qty = o.Sum(x => x.Qty) });
                foreach (var item in g)
                {
                    Console.WriteLine($"{item.Key}-{item.Qty}");
                }
                
            }
        }

        static void InnerJoin()
        {
            using (var dbContext = GetDbContext())
            {
                var outerOrder = dbContext.Orders;
                var innerItem = dbContext.OrderItems;

                var list = outerOrder.Join(innerItem, o => o.Id, oi => oi.OrderId,
                    (o, oi) => new
                    {
                        o.Id,
                        o.OrderNo,
                        oi.ProductName
                    });
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.OrderNo}-{item.ProductName}");
                }

                Console.WriteLine("=========================");
                // 通过Include查两次数据库
                //var orders = dbContext.Orders.Include(o => o.Items).ToList();
                //foreach (var o in orders)
                //{
                //    Console.WriteLine(o.OrderNo);
                //    foreach (var oi in o.Items)
                //    {
                //        Console.WriteLine($"\t{oi.ProductName}");
                //    }
                //}
            }
        }

        static void LeftJoin()
        {
            using (var dbContext = GetDbContext())
            {
                var outerOrder = dbContext.Orders;
                var innerItem = dbContext.OrderItems;

                var list = outerOrder.GroupJoin(innerItem, o => o.Id, oi => oi.OrderId,
                    (o, oi) => new
                    {
                        o.Id,
                        o.OrderNo,
                        OrderItems = oi
                    });
                foreach (var item in list)
                {
                    Console.WriteLine($"{item.OrderNo}");
                    foreach (var oi in item.OrderItems)
                    {
                        Console.WriteLine($"\t{oi.ProductName}");
                    }
                }

                
            }
        }

        static void Concat()
        {
            using (var dbContext = GetDbContext())
            {
                var first = dbContext.Orders.Where(o => o.Id == 1);
                var second = dbContext.Orders.Where(o => o.Id == 2);

                var orders = first.Concat(second)
                    .Select(o =>
                    new { o.OrderNo });
                foreach (var item in orders)
                {
                    Console.WriteLine(item.OrderNo);                        
                }
            }
        }

        /// <summary>
        /// 谓词
        /// </summary>
        static void PredicateTest()
        {
            using (var dbContext = GetDbContext())
            {
                /*
                 * Contains翻译成IN
                 SELECT CASE
                      WHEN @__p_0 IN (
                          SELECT [o].[OrderNo]
                          FROM [Order] AS [o]
                      )
                      THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
                  END
                 */
                var orders = dbContext.Orders.Select(o => o.OrderNo).Contains("PM4444");

                /*字符串的Contains翻译成CHARINDEX
                 SELECT [o].[Id], [o].[CreateTime], [o].[OrderNo], [o].[TotalAmount], [o].[UserName]
                  FROM [Order] AS [o]
                  WHERE CHARINDEX(N'11', [o].[OrderNo]) > 0
                 */
                dbContext.Orders.Where(o => o.OrderNo.Contains("11")).ToList();

                /*
                 * SELECT [o].[Id], [o].[CreateTime], [o].[OrderNo], [o].[TotalAmount], [o].[UserName]
                  FROM [Order] AS [o]
                  WHERE [o].[OrderNo] IN ('11', '44', '55')
                 */
                dbContext.Orders.Where(o => new string[] { "11", "44", "55" }.Contains(o.OrderNo)).ToList();

                /*Any翻译成Exists
                 SELECT CASE
                      WHEN EXISTS (
                          SELECT 1
                          FROM [Order] AS [o]
                          WHERE [o].[TotalAmount] > 200.0)
                      THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
                  END
                 */
                dbContext.Orders.Any(o => o.TotalAmount > 200);

                /*All翻译成NOT EXISTS
                 SELECT CASE
                      WHEN NOT EXISTS (
                          SELECT 1
                          FROM [Order] AS [o]
                          WHERE [o].[TotalAmount] <= 10.0)
                      THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
                  END
                 */
                dbContext.Orders.All(o => o.TotalAmount > 10);
            }
        }

        static void ThreeLoad()
        {
            // 饥饿加载（Eager Loading）：Include

            // 显示加载（Explicitly Loading）
            // 只有关闭延迟加载如下代码才有用：builder.UseLazyLoadingProxies(false);
            using (var dbContext = GetDbContext())
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.Id == 1);
                if(order.Items == null)
                {
                    Console.WriteLine("OrderItems并没有加载出来");
                    dbContext.Entry(order).Collection(o => o.Items)
                        .Load();
                    if (order.Items != null)
                    {
                        Console.WriteLine("OrderItems通过Collection的Load显示加载出来了");
                    }
                }
                
            }

            Console.WriteLine("==========================");

            // 延迟加载（Lazy Loading）
            // 必须启用 builder.UseLazyLoadingProxies();
            using (var dbContext = GetDbContext())
            {
                var order = dbContext.Orders.FirstOrDefault(o => o.Id == 1);
                // 如果没有用到order.Items属性是不会去查询数据库的
                //var items = order.Items;
            }
        }

        static void Sql()
        {
            using (var dbContext = GetDbContext())
            {
                // 使用sql语句查询时必须返回所有列
                //dbContext.Orders.FromSql<Order>("select * from dbo.[Order]").ToList();

                string orderNo = "PM'111";
                FormattableString sql = $@"select * from dbo.[Order] where OrderNo = {orderNo}";
                dbContext.Orders.FromSql(sql).ToList();

                /*
                 * 执行insert语句
                string commandText = "insert into .... values(@Name,@Time)";
                var paramts = new SqlParameter[]
                {
                    new SqlParameter("@Name",SqlDbType.NVarChar),
                    new SqlParameter("@Time",SqlDbType.DateTime),
                };
                paramts[0].Value = "hh";
                paramts[1].Value = DateTime.Now;
                dbContext.Database.ExecuteSqlCommand(commandText, paramts);
                */
            }
        }

        static void BatchInsert()
        {
            using (var dbContext = GetDbContext())
            {
                List<Order> orders = new List<Order>();
                for (int i = 1; i <= 2000; i++)
                {
                    orders.Add(new Order { OrderNo = $"PM{i}", TotalAmount = 100, CreateTime = DateTime.Now, UserName = $"test{i}" });
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                dbContext.Orders.AddRange(orders);
                dbContext.SaveChanges();
                sw.Stop();
                Console.WriteLine($"耗时：{sw.ElapsedMilliseconds}");
            }
        }

        static void Like()
        {
            using (var dbContext = GetDbContext())
            {
                /*
                 * SELECT [o].[Id], [o].[CreateTime], [o].[OrderNo], [o].[TotalAmount], [o].[UserName]
                  FROM [Order] AS [o]
                  WHERE [o].[OrderNo] LIKE 'PM' + '%' AND (LEFT([o].[OrderNo], LEN(N'PM')) = 'PM')
                 */
                dbContext.Orders.Where(o => o.OrderNo.StartsWith("PM")).ToList();

                /*
                 * SELECT [o].[Id], [o].[CreateTime], [o].[OrderNo], [o].[TotalAmount], [o].[UserName]
                  FROM [Order] AS [o]
                  WHERE RIGHT([o].[OrderNo], LEN(N'2')) = '2'
                 */
                dbContext.Orders.Where(o => o.OrderNo.EndsWith("2")).ToList();

                /*
                 SELECT [o].[Id], [o].[CreateTime], [o].[OrderNo], [o].[TotalAmount], [o].[UserName]
                  FROM [Order] AS [o]
                  WHERE [o].[OrderNo] LIKE '%22'
                 */
                dbContext.Orders.Where(o => EF.Functions.Like(o.OrderNo, "%22")).ToList();

                // 定义参数，EF会缓存生成的sql语句，不用每次都翻译成sql
                string query = "34";
                dbContext.Orders.Where(o => EF.Functions.Like(o.OrderNo, $"[{query}]%")).ToList();

                // 查询的字段含有特殊符号，需要转义
                dbContext.Orders.Where(o => EF.Functions.Like(o.OrderNo, @"%\22", @"\")).ToList();

            }
        }

        static void CompileQuery()
        {
            var query = EF.CompileQuery((EfCoreDbContext db, int id) =>
                 db.Orders.FirstOrDefault(o => o.Id == id)
            );

            using (var dbContext = GetDbContext())
            {
                query(dbContext, 1);
                query(dbContext, 2);
            }
        }
    }
}
