using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _10._3.第三方扩展模型验证.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public decimal TotalAmount { get; set; }
        public string UserName { get; set; }
    }

    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(o => o.OrderNo).NotEmpty().WithMessage("订单编号不能为空")
                .MaximumLength(10).WithMessage("订单编号不能超过10位");
        }
    }
}
