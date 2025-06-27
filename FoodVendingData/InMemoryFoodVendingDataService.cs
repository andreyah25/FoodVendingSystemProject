using System.Collections.Generic;
using System.Linq;
using VendingCommon;

namespace FoodVendingData
{
    public class InMemoryFoodDataService : IFoodVendingDataService
    {
        private readonly List<SnackItem> inventory = new List<SnackItem>
        {
            new SnackItem { Name = "Piattos", Price = 35.25, Quantity = 10 },
            new SnackItem { Name = "Vcut", Price = 49.25, Quantity = 8 },
            new SnackItem { Name = "Cheesy", Price = 45.00, Quantity = 15 },
            new SnackItem { Name = "Pic A", Price = 36.50, Quantity = 20 }
        };

        public List<SnackItem> LoadItems()
        {
            return inventory.Select(item => new SnackItem
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();
        }

        public SnackItem GetItemByName(string name)
        {
            return inventory.FirstOrDefault(item => item.Name.ToLower() == name.ToLower());
        }

        public bool AddItem(SnackItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name) || item.Price <= 0 || item.Quantity <= 0)
                return false;


            if (inventory.Any(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                return false; // Item already exists

            

            inventory.Add(item);
            return true;
        }

        public bool RemoveItem(string name)
        {
            var item = GetItemByName(name);
            if (item == null) return false;

            inventory.Remove(item);
            return true;
        }

        public bool UpdateItemQuantity(string name, int deltaQuantity)
        {
            var item = GetItemByName(name);
            if (item == null) return false;

            int newQty = item.Quantity + deltaQuantity;
            if (newQty < 0) return false;

            item.Quantity = newQty;
            return true;
        }

        public List<SnackItem> GetAllItems()
        {
            return LoadItems();
        }

        public bool AddNewItem(SnackItem item)
        {
            return AddItem(item);
        }

    }
}
