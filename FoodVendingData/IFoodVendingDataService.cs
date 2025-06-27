<<<<<<< HEAD
﻿using System.Collections.Generic;
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
=======
﻿using System.Collections.Generic;
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
>>>>>>> 1bf1ccf10240483bb6f0ffc9c613fb156e742f61
