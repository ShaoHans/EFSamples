using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.NetCore.Entities
{
    public class Order: BaseEntity
    {
        public string OrderNo { get; set; }

        public decimal TotalAmount { get; set; }

        // 内置模型验证
        [Required(ErrorMessage = "用户名不能为空"), MaxLength(8,ErrorMessage = "用户名长度不能超过10")]
        public string UserName { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
