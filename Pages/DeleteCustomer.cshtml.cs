using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class DeleteCustomerModel : PageModel
    {
        [BindProperty]
        [Required]
        public string customerName { get; set; }

        [BindProperty]
        [Required]
        public string address { get; set; }

        [BindProperty]
        [Required]
        public string city { get; set; }

        [BindProperty]
        [Required]
        public string province { get; set; }

        [BindProperty]
        [Required]
        public string postalCode { get; set; }

        [BindProperty]
        public Model.Customer ABCCustomer { get; set; }

        public bool Confirmation;

        [TempData] public string Alert { get; set; }

        Controller.ABCPOS ABCHardware = new Controller.ABCPOS();
        public void OnGet()
        {
            int id = int.Parse(Request.Query["id"].ToString());

            ABCCustomer = ABCHardware.FindCustomer(id);
        }

        public ActionResult OnPost()
        {
            int id = int.Parse(Request.Query["id"].ToString());
            ABCCustomer.CustomerID = id;           

            Confirmation = ABCHardware.RemoveCustomer(id);

            if (Confirmation)
            {
                Alert = $"Customer Deleted Successfully";

                return RedirectToPage("Customers");
            }

            return Page();
        }
    }
}