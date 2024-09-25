using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.BusinessModel
{
    public class UpdateOrderModel
    {
        public decimal? TotalAmount { get; set; }

        public string OrderStatus { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
