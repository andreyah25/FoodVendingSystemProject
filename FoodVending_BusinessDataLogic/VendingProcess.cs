using VendingCommon;
using FoodVendingData;
using System;
using System.Collections.Generic;

namespace VendingSystem_BusinessDataLogic
{
    public class VendingProcess
    {
        private IFoodVendingDataService dataService;
        private double cardBalance = 100.00;
        public double GetBalance() => cardBalance;

        public VendingProcess()
        {
            dataService = new TextFileDataService();
        }

        public bool ValidateAdminPIN(int adminPIN) => adminPIN == 1234;

        public bool ValidatePIN(int userPIN) => userPIN == 2005;

        public bool IsItemInStock(string name)
        {
            var inventory = dataService.GetInventory();
            var item = inventory.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return item != null && item.Quantity > 0;
        }

        public string[] GetInventoryDetails()
        {
            var inventory = dataService.GetInventory();
            if (inventory.Count == 0)
                return new[] { "No items available." };

            var details = new List<string>();
            foreach (var item in inventory)
            {
                details.Add($"{item.Name} - PHP {item.Price:F2} (Stock: {item.Quantity})");
            }
            return details.ToArray();
        }

        public bool RestockItem(string name, int quantity)
        {
            var inventory = dataService.GetInventory();
            var item = inventory.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (item != null && quantity > 0)
            {
                item.Quantity += quantity;
                dataService.UpdateQuantity(item);
                return true;
            }
            return false;
        }

        public bool AddNewItem(string name, double price, int quantity)
        {
            if (!string.IsNullOrWhiteSpace(name) && price > 0 && quantity > 0)
            {
                var existing = dataService.GetInventory().Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (existing == null)
                {
                    var newItem = new VendingItem { Name = name, Price = price, Quantity = quantity };
                    dataService.AddSnack(newItem);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteItem(string name)
        {
            var inventory = dataService.GetInventory();
            var item = inventory.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                dataService.RemoveItem(item);
                return true;
            }
            return false;
        }

        public string SearchItem(string name)
        {
            return dataService.SearchItem(name);
        }

        public bool PurchaseItem(string name)
        {
            var inventory = dataService.GetInventory();
            var item = inventory.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (item != null && item.Quantity > 0 && cardBalance >= item.Price)
            {
                item.Quantity--;
                cardBalance -= item.Price;
                dataService.UpdateQuantity(item);
                return true;
            }

            return false;
        }


        public bool AddFunds(double amount)
        {
            if (amount > 0)
            {
                cardBalance += amount;
                return true;
            }
            return false;
        }
    }
}
