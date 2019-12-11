﻿using System;
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
        [Required (ErrorMessage = "Customer Name is required!")]
        public string ItemCode { get; set; }

        public Model.Item inventoryItem { get; set; }

        [TempData] public string Alert { get; set; }

        public void OnPost()
        {
            Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

            inventoryItem = ABCHardware.FindItem(ItemCode);
        }
    }
}