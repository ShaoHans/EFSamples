using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Entities
{
    public class SqlLog : BaseEntity
    {
        public string Sql { get; set; }

        public string Parameters { get; set; }

        public string CommandType { get; set; }

        public long Milliseconds { get; set; }

        public string Exception { get; set; }
    }
}
