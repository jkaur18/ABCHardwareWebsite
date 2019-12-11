using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class DeleteItemModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Itemcode { get; set; }

        [BindProperty]
        [Required]
        public string Description { get; set; }

        [BindProperty]
        [Required]
        public decimal Unitprice { get; set; }

        [BindProperty]
        public Model.Item InventoryItem { get; set; }

        public bool Confirmation;

        [TempData] public string Alert { get; set; }

        Controller.ABCPOS ABCHardware = new Controller.ABCPOS();
        public void OnGet()
        {
            string id = Request.Query["id"];

            InventoryItem = ABCHardware.FindItem(id);
        }

        public ActionResult OnPost()
        {
            string id = Request.Query["id"];
            Confirmation = ABCHardware.RemoveItem(id);

            if (Confirmation)
            {
                Alert = $"Item Deleted Successfully";

                return RedirectToPage("Items");
            }

            return Page();
        }
    }
}