using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetCore.Entities
{
    public class Order: BaseEntity
    {
        public string OrderNo { get; set; }

        public decimal TotalAmount { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
