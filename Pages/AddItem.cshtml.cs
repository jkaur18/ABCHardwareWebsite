using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty]
        [Required (ErrorMessage = "Item Code is required!" )]
        public string Itemcode { get; set; }

        [BindProperty]
        [Required (ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        [BindProperty]
        [Required (ErrorMessage = "Unit Price is required!")]
        public decimal Unitprice { get; set; }

        [BindProperty]
        [Required (ErrorMessage = "Quantity on Hand is required!")]
        public int QtyOH { get; set; }

        [BindProperty]
        public Model.Item InventoryItem { get; set; }

        public bool Confirmation;

        [TempData] public string Alert { get; set; }
      
        public ActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                InventoryItem.ItemCode = Itemcode;
                InventoryItem.Description = Description;
                InventoryItem.UnitPrice = Unitprice;
                InventoryItem.QtyOH = QtyOH;

                Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

                Confirmation = ABCHardware.CreateItem(InventoryItem);

                if (Confirmation)
                {
                    Alert = $"Item Added Successfully";

                    return RedirectToPage("AddItem");
                }               
            }
            return Page();
        }

    }
}