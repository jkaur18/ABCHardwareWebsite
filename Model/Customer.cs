using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JKaur18ABCHardwareWebsite.Model
{
    public class Customer
    {
       
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }
    }
}
