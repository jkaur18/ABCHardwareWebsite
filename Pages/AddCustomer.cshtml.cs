using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class AddCustomerModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Customer Name is required!")]
        public string CustomerName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Province is required!")]
        public string Province { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postal Code is required!")]
        public string PostalCode { get; set; }

        [BindProperty]
        public Model.Customer newCustomer { get; set; }

        public bool Confirmation;

        [TempData] public string Alert { get; set; }

        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                newCustomer.CustomerName = CustomerName;
                newCustomer.Address = Address;
                newCustomer.City = City;
                newCustomer.Province = Province;
                newCustomer.PostalCode = PostalCode;

                Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

                Confirmation = ABCHardware.CreateCustomer(newCustomer);

                if (Confirmation)
                {
                    Alert = $"Customer Added Successfully";

                    return RedirectToPage("AddCustomer");
                }
            }
            return Page();
        }
    }
}