using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class UpdateItemModel : PageModel
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
        public bool Active { get; set; }

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
            InventoryItem.ItemCode = id;
            InventoryItem.Description = Description;
            InventoryItem.UnitPrice = Unitprice;

            Confirmation = ABCHardware.EditItem(InventoryItem);

            if (Confirmation)
            {
                Alert = $"Item Updated Successfully";

                return RedirectToPage("Items");
            }

            return Page();
        }
    }
}
