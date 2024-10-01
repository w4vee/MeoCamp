using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public virtual User Customer { get; set; }
    }
}
