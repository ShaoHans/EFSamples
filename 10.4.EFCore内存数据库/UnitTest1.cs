using Infrastructure.NetCore;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace _10._4.EFCore内存数据库
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            using (var dbContext = InitContext())
            {
                var order = dbContext.Orders.Find(1);
                Assert.NotNull(order);
                Assert.Equal(1, order.Id);
            }
        }

        private EfCoreDbContext InitContext()
        {
            var options = new DbContextOptionsBuilder<EfCoreDbContext>()
                .UseInMemoryDatabase(databaseName: "test")
                .Options;

            var dbContext = new EfCoreDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

    }
}
