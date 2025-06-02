using System.Collections.Generic;
using VendingCommon;

namespace FoodVendingData
{
    public interface IFoodVendingDataService 
    {
        List<SnackItem> LoadItems();                   
        SnackItem GetItemByName(string name);          
        bool AddItem(SnackItem item);                 
        bool RemoveItem(string name);                   
        bool UpdateItemQuantity(string name, int deltaQuantity); 
        List<SnackItem> GetAllItems();                
        bool AddNewItem(SnackItem item);                 
    }
}
