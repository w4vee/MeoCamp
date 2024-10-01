using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.BusinessModel
{
    public class RentalModel
    {
        public int ProductId { get; set; }

        public DateTime RentalStartDate { get; set; }

        public DateTime? RentalEndDate { get; set; }

        public string Description { get; set; }

        public string RentalStatus { get; set; }
    }
}
