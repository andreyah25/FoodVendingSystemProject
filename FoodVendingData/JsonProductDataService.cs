using System.Text.Json;
using VendingCommon;

namespace FoodVendingData
{
    public class JsonProductDataService : IFoodVendingDataService
    {
        private static List<SnackItem> snackItems = new();
        private static string jsonFilePath = "inventory.json";

        public JsonProductDataService()
        {
            LoadFromFile();
        }

        public JsonProductDataService(string path)
        {
            jsonFilePath = path;
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonText = File.ReadAllText(jsonFilePath);
                snackItems = JsonSerializer.Deserialize<List<SnackItem>>(jsonText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<SnackItem>();
            }
            else
            {
                snackItems = new List<SnackItem>();
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            string jsonString = JsonSerializer.Serialize(snackItems,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, jsonString);
        }

        private int FindItemIndex(string name)
        {
            return snackItems.FindIndex(item =>
                item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<SnackItem> LoadItems()
        {
            return snackItems.Select(item => new SnackItem
            {
                Name = item.Name,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();
        }

        public SnackItem GetItemByName(string name)
        {
            return snackItems.FirstOrDefault(item =>
                item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool AddItem(SnackItem item)
        {
            if (FindItemIndex(item.Name) != -1)
                return false;

            snackItems.Add(item);
            SaveToFile();
            return true;
        }

        public bool RemoveItem(string name)
        {
            var index = FindItemIndex(name);
            if (index != -1)
            {
                snackItems.RemoveAt(index);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool UpdateItemQuantity(string name, int deltaQuantity)
        {
            var index = FindItemIndex(name);
            if (index != -1)
            {
                snackItems[index].Quantity += deltaQuantity;
                SaveToFile();
                return true;
            }
            return false;
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
