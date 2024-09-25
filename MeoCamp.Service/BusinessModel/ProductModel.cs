using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.BusinessModel
{
    public class ProductModel
    {

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double? Price { get; set; }

        public double? RentalPrice { get; set; }

        public bool? IsRentable { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool? Status { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }
    }
}
