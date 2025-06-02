using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VendingCommon;

namespace FoodVendingData
{
    public class TextFileDataService : IFoodVendingDataService
    {
        private readonly string filePath;

        public TextFileDataService(string filePath = "inventory.txt")
        {
            this.filePath = filePath;

          
            if (!File.Exists(this.filePath))
                File.WriteAllText(this.filePath, string.Empty);
        }

        public List<SnackItem> LoadItems()
        {
            var items = new List<SnackItem>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length != 3)
                    continue;

                string name = parts[0];
                if (!double.TryParse(parts[1], out double price))
                    continue;
                if (!int.TryParse(parts[2], out int qty))
                    continue;

                items.Add(new SnackItem
                {
                    Name = name,
                    Price = price,
                    Quantity = qty
                });
            }

            return items;
        }

        public SnackItem GetItemByName(string name)
        {
            var allItems = LoadItems();
            return allItems.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool AddItem(SnackItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name) || item.Price <= 0 || item.Quantity <= 0)
                return false;

            var items = LoadItems();

            if (items.Any(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                return false;

            items.Add(item);
            return SaveItems(items);
        }

        public bool RemoveItem(string name)
        {
            var items = LoadItems();
            var item = items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (item == null)
                return false;

            items.Remove(item);
            return SaveItems(items);
        }

        public bool UpdateItemQuantity(string name, int deltaQuantity)
        {
            var items = LoadItems();
            var item = items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (item == null)
                return false;

            int newQty = item.Quantity + deltaQuantity;
            if (newQty < 0)
                return false;

            item.Quantity = newQty;
            return SaveItems(items);
        }

        
        public List<SnackItem> GetAllItems() => LoadItems();

        public bool AddNewItem(SnackItem item) => AddItem(item);

       
        private bool SaveItems(List<SnackItem> items)
        {
            try
            {
                var lines = items.Select(i => $"{i.Name}|{i.Price}|{i.Quantity}");
                File.WriteAllLines(filePath, lines);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
