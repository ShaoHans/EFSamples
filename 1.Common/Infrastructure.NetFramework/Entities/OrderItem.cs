using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetFramework.Entities
{
    public class OrderItem : BaseEntity
    {
        public string ProductName { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
