using VendingCommon;
using FoodVendingData;
using System;
using System.Linq;
using VendingDataService;

namespace FoodVending_BusinessLogic
{
    public class VendingProcess : TextFileDataService
    {
        private readonly TextFileDataService _dataService;
        private double _balance = 120.25;
        private readonly int _adminPIN = 0525;
        private readonly int _userPIN = 2005;

        public VendingProcess()
        {

            //_dataService = new InMemoryFoodDataService();
             _dataService = new TextFileDataService("inventory.txt");
            //_dataService = new JsonProductDataService("inventory.json");
            //_dataService = new DBFoodVendingDataService();
        }

        public bool ValidatePIN(int pin) => pin == _userPIN;
        public bool ValidateAdminPIN(int pin) => pin == _adminPIN;
        public double GetBalance() => _balance;

        public bool AddFunds(double amount)
        {
            if (amount <= 0) return false;
            _balance += amount;
            return true;
        }

        public string[] GetInventoryDetails()
        {
            var items = _dataService.LoadItems();
            return items.Select(i => $"{i.Name} - PHP {i.Price:F2} - Qty: {i.Quantity}").ToArray();
        }

        public bool AddNewItem(string name, double price, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name) || price <= 0 || quantity < 0)
                return false;

            var item = new SnackItem { Name = name.Trim(), Price = price, Quantity = quantity };
            return _dataService.AddItem(item);
        }

        public bool DeleteItem(string name)
        {
            return _dataService.RemoveItem(name.Trim());
        }

        public string SearchItem(string name)
        {
            var item = _dataService.GetItemByName(name.Trim());
            return item != null
                ? $"Item found: {item.Name} - PHP {item.Price:F2} - Qty: {item.Quantity}"
                : "Item not found.";
        }

        public bool RestockItem(string name, int quantity)
        {
            if (quantity <= 0) return false;
            return _dataService.UpdateItemQuantity(name.Trim(), quantity);
        }

        public bool PurchaseItem(string name)
        {
            var item = _dataService.GetItemByName(name.Trim());
            if (item == null || item.Quantity <= 0 || item.Price > _balance)
                return false;

            bool updated = _dataService.UpdateItemQuantity(name.Trim(), -1);
            if (!updated)
                return false;

            _balance -= item.Price;
            return true;
        }
    }
}
