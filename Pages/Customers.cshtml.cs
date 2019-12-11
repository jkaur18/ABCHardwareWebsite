using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class CustomersModel : PageModel
    {
        [BindProperty]
        [Required (ErrorMessage = "Customer ID is required!")]
        public int CustomerID { get; set; }

        public Model.Customer ABCCustomer { get; set; }

        public bool foundcustomer { get; set; }

        [TempData] public string Alert { get; set; }

        public void OnPost()
        {
            Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

            ABCCustomer = ABCHardware.FindCustomer(CustomerID);

            foundcustomer = ABCCustomer.CustomerID != 0 ? true : false;
        }

    }
}