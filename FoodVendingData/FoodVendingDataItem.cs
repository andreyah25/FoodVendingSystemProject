using System;
using System.Collections.Generic;

namespace FoodVendingData
{
    public class FoodVendingDataItem
    {
        //DATA ITEMS IN THE VENDING
        public static List<string> itemNames = new List<string> { "Piattos", "Vcut", "Cheesy", "Pic A" };
        public static List<double> itemPrices = new List<double> { 35.00, 38.25, 40.99, 50.88 };
        public static List<int> itemQuantities = new List<int> { 8, 5, 13, 11 };

        // PIN OF USER AND OWNER
        public static readonly int correctPIN = 2245;
        public static readonly int ownerPIN = 2005;
        public static bool RemoveSnackFromInventory(int index)
        {
            if (index >= 0 && index < itemNames.Count)
            {
                itemNames.RemoveAt(index);
                itemPrices.RemoveAt(index);
                itemQuantities.RemoveAt(index);
                return true;
            }
            return false;
        }
        public static string SearchItemName(string name)
        {
            int index = itemNames.FindIndex(n => n.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                return $"{itemNames[index]} - PHP {itemPrices[index]:F2} (Stock: {itemQuantities[index]})";
            }
            return "Item not found..";
            
        }
       
    }
}
