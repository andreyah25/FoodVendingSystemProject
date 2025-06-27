using System.Collections.Generic;
using VendingCommon;
using VendingDataService;

namespace FoodVendingData
{
    public class FoodVendingDataItem
    {
        IFoodVendingDataService dataService;

        public FoodVendingDataItem()
        {

            // dataService = new InMemoryFoodDataService();
            // dataService = new TextFileDataService("inventory.txt");
           // dataService = new JsonProductDataService("inventory.json");
            dataService = new DBFoodVendingDataService();
        }

        public List<SnackItem> GetAllItems()
        {
            return dataService.LoadItems();
        }

        public SnackItem GetItemByName(string name)
        {
            return dataService.GetItemByName(name);
        }

        public bool AddNewItem(SnackItem item)
        {
            return dataService.AddItem(item);
        }

        public bool RemoveItem(string name)
        {
            return dataService.RemoveItem(name);
        }

        public bool UpdateItemQuantity(string name, int deltaQuantity)
        {
            return dataService.UpdateItemQuantity(name, deltaQuantity);
        }
    }
}
