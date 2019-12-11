using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JKaur18ABCHardwareWebsite.Controller
{
    public class ABCPOS
    {
        Model.Items ItemManager = new Model.Items();
        Model.Customers CustomerManager = new Model.Customers();
        public bool CreateItem (Model.Item Inventoryitem)
        {
            bool Confirmation;

            Confirmation = ItemManager.AddItem(Inventoryitem);

            return Confirmation;
        }

        public bool EditItem(Model.Item Inventoryitem)
        {
            bool Confirmation;            

            Confirmation = ItemManager.UpdateItem(Inventoryitem);

            return Confirmation;
        }

        public bool RemoveItem(string itemcode)
        {
            bool Confirmation;

            Confirmation = ItemManager.DeleteItem(itemcode);

            return Confirmation;
        }

        public Model.Item FindItem(string itemcode)
        {
            Model.Item inventoryItem = new Model.Item();

            inventoryItem = ItemManager.GetItem(itemcode);

            return inventoryItem;
        }

        public bool CreateCustomer(Model.Customer newCustomer)
        {
            bool Confirmation;

            Confirmation = CustomerManager.AddCustomer(newCustomer);

            return Confirmation;
        }

        public bool EditCustomer(Model.Customer newCustomer)
        {
            bool Confirmation;

            Confirmation = CustomerManager.UpdateCustomer(newCustomer);

            return Confirmation;
        }

        public bool RemoveCustomer(int customerid)
        {
            bool Confirmation;

            Confirmation = CustomerManager.DeleteCustomer(customerid);

            return Confirmation;
        }

        public Model.Customer FindCustomer(int customerid)
        {
            Model.Customer ABCCustomer = new Model.Customer();

            ABCCustomer = CustomerManager.GetCustomer(customerid);

            return ABCCustomer;
        }

    }
}
