
﻿using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string User_name { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
