using System.Collections.Generic;

namespace VendingCommon
{
    public interface IFoodVendingDataService
    {
        List<VendingItem> GetInventory(); 
        void AddSnack(VendingItem item);  
        bool RemoveItem(VendingItem item);
        void UpdateQuantity(VendingItem item);
        string SearchItem(string name);
    }
}
