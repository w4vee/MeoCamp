using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? Post_date { get; set; }

        public string? Image { get; set; }
        public bool Status { get; set; }
        public virtual User Customer { get; set; }
    }
}
