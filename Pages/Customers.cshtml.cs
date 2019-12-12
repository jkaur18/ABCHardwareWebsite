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
        [Required (ErrorMessage = "Customer Name is required!")]
        public string CustomerName { get; set; }

        public Model.Customer ABCCustomer { get; set; }

        public bool foundcustomer { get; set; }

        [TempData] public string Alert { get; set; }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

                ABCCustomer = ABCHardware.FindCustomer(CustomerName);

                foundcustomer = ABCCustomer.CustomerName != null ? true : false;
            }
            return Page();
        }

    }
}