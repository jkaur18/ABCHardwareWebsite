using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JKaur18ABCHardwareWebsite.Pages
{
    public class ProcessSaleModel : PageModel
    {  

        
        [BindProperty]
        public List<Model.Item> ItemList { get; set; }

        Controller.ABCPOS ABCHardware = new Controller.ABCPOS();

        public void OnGet()
        {
            ItemList = ABCHardware.FindItems();
        }

        public void OnPost()
        {

        }
       
    }
}