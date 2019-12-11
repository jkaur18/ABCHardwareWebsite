using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JKaur18ABCHardwareWebsite.Model
{
    public class Item
    {
        public string ItemCode { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; } 

        public bool Deleted { get; set; }
    }
}
