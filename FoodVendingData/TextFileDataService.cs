using VendingCommon;
using System;
using System.Collections.Generic;
using System.IO;

namespace FoodVendingData
{
    public class TextFileDataService : IFoodVendingDataService
    {
     string filePath = "inventory.txt";
     List<VendingItem> items = new List<VendingItem>();

        public TextFileDataService()
        {
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            items.Clear();
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');

                    if (parts.Length == 3 &&
                        double.TryParse(parts[1], out double price) &&
                        int.TryParse(parts[2], out int quantity))
                    {
                        items.Add(new VendingItem
                        {
                            Name = parts[0],
                            Price = price,
                            Quantity = quantity
                        });
                    }
                }
            if (items.Count == 0)
            {
                items.Add(new VendingItem { Name = "Piattos", Price = 35.00, Quantity = 8 });
                items.Add(new VendingItem { Name = "Vcut", Price = 38.25, Quantity = 5 });
                items.Add(new VendingItem { Name = "Cheesy", Price = 40.99, Quantity = 13 });
                items.Add(new VendingItem { Name = "Pic A", Price = 50.88, Quantity = 11 });
                WriteToFile();
            }
        }

        private void WriteToFile()
        {
            var lines = new string[items.Count];
            for (int i = 0; i < items.Count; i++)
            {
                lines[i] = $"{items[i].Name}|{items[i].Price}|{items[i].Quantity}";
            }
            File.WriteAllLines(filePath, lines);
        }


        public int FindIndex(VendingItem item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }
        public List<VendingItem> GetInventory()
        {
            return items;
        }
        public void AddSnack(VendingItem item)
        {
            items.Add(item);
            WriteToFile();
        }

        void UpdateQuantity(VendingItem item)
        {
            int index = FindIndex(item);
            if (index != -1)
            {
                items[index].Price = item.Price;
                items[index].Quantity = item.Quantity;
                WriteToFile();
            }

        }

        bool IFoodVendingDataService.RemoveItem(VendingItem item) 
        {
            int index = -1;
            if (index != -1)
            {
                items.RemoveAt(index);
                WriteToFile();
            }
            return false;
        }

        public string SearchItem(string name) 
        {
            var found = items.Find(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return found != null
                ? $"{found.Name} - PHP {found.Price:F2} (Stock: {found.Quantity})"
                : "Item not found.";
        }
      void IFoodVendingDataService.UpdateQuantity(VendingItem item)
        {
           
        }
    }
}
