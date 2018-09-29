using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetCore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
