using FoodVendingData; // Add this at the top to access shared data

namespace VendingSystem_BusinessDataLogic
{
    public class VendingProcess
    {
        private static double cardBalance = 100.00;

        public static bool ValidateAdminPIN(int adminPIN) => adminPIN == FoodVendingDataItem.ownerPIN;

        public static bool ValidatePIN(int userPIN) => userPIN == FoodVendingDataItem.correctPIN;

        public static string[] GetItemDetails(string itemNumber)
        {
            if (int.TryParse(itemNumber, out int index))
            {
                index -= 1;
                if (index >= 0 && index < FoodVendingDataItem.itemNames.Count)
                {
                    return new string[]
                    {
                        FoodVendingDataItem.itemNames[index],
                        FoodVendingDataItem.itemPrices[index].ToString("F2")
                    };
                }
            }
            return null;
        }

        public static bool SufficientBalance(double price) => cardBalance >= price;

        public static (bool success, double remainingBalance) ProcessPurchase(string itemName, double price)
        {
            int index = FoodVendingDataItem.itemNames.IndexOf(itemName);
            if (index != -1 && FoodVendingDataItem.itemQuantities[index] > 0 && SufficientBalance(price))
            {
                FoodVendingDataItem.itemQuantities[index]--;
                cardBalance -= price;
                return (true, cardBalance);
            }
            return (false, cardBalance);
        }

        public static double GetBalance() => cardBalance;

        public static bool AddFunds(double amount)
        {
            if (amount > 0)
            {
                cardBalance += amount;
                return true;
            }
            return false;
        }

        public static bool IsItemInStock(string itemName)
        {
            int index = FoodVendingDataItem.itemNames.IndexOf(itemName);
            return index != -1 && FoodVendingDataItem.itemQuantities[index] > 0;
        }
        //READ
        public static string[] GetInventory()
        {
            string[] inventory = new string[FoodVendingDataItem.itemNames.Count];
            for (int i = 0; i < inventory.Length; i++)
            {
                inventory[i] = $"{FoodVendingDataItem.itemNames[i]} - PHP {FoodVendingDataItem.itemPrices[i]:F2} (Stock: {FoodVendingDataItem.itemQuantities[i]})";
            }
            return inventory;
        }
        //UPADTE
        public static bool RestockItems(string itemName, int quantity)
        {
            int index = FoodVendingDataItem.itemNames.IndexOf(itemName);
            if (index != -1 && quantity > 0)
            {
                FoodVendingDataItem.itemQuantities[index] += quantity;
                return true;
            }
            return false;
        }
        //CREATE
        public static bool AddSnack(string name, double price, int quantity)
        {
            if (!string.IsNullOrWhiteSpace(name) && price > 0 && quantity > 0)
            {
                FoodVendingDataItem.itemNames.Add(name);
                FoodVendingDataItem.itemPrices.Add(price);
                FoodVendingDataItem.itemQuantities.Add(quantity);
                return true;
            }
            return false;
        }
        //DELETE
        public static bool DeleteSnack(string name)
        {
            int index = FoodVendingDataItem.itemNames.FindIndex(n => n.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (index == -1)
            {
                return FoodVendingDataItem.RemoveSnackFromInventory(index);

            }
            return false;

        }
        //SEARCH 
        public static string SearchSnack(string name)
        {
            return FoodVendingDataItem.SearchItemName(name);
        }
    }
}
