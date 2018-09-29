using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7._1.预编译视图
{
    public class MyDbConfiguration:DbConfiguration
    {
        public MyDbConfiguration()
        {
            // EF6.2+可通过如下代码生成视图
            SetModelStore(new DefaultDbModelStore(Directory.GetCurrentDirectory()));
        }
    }
}
