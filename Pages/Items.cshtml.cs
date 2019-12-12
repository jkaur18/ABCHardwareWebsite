using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class ItemsModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Item Code is required!")]
        public string ItemCode { get; set; }

        public Model.Item inventoryItem { get; set; }

        [TempData] public string Alert { get; set; }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

                inventoryItem = ABCHardware.FindItem(ItemCode);
            }
            return Page();
        }
    }
}