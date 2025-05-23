using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingCommon;

namespace FoodVendingData
{
    public class InMemoryFoodVendingDataService : IFoodVendingDataService
    {
        private List<VendingItem> item= new List<VendingItem>();
        public InMemoryFoodVendingDataService()
        {
            ListOfAvailableItems();
        }
        private void ListOfAvailableItems()
        {
            VendingItem item1 = new VendingItem();
            item1.Name = "Piattos";
            item1.Price = 30.00;
            item1.Quantity = 8;

            item.Add(item1);

            VendingItem item2 = new VendingItem
            {
                Name = "Vcut",
                Price = 38.25,
                Quantity = 5
            };
            item.Add(item2);

            item.Add(new VendingItem
            {
                Name = "Cheesy",
                Price = 40.99,
                Quantity = 13
            });

            item.Add(new VendingItem
            {
                Name = "Pic A",
                Price = 50.88,
                Quantity = 11
            });



        }
        public List<VendingItem> GetInventory()
        {
            return item;
        }

        public void AddSnack(VendingItem vendingItem)
        {
            
        }

        public void RemoveItem(VendingItem vendingItem)
        {
            
        }

        public void UpdateQuantity(VendingItem vendingItem)
        {
           for (int i = 0; i < item.Count; i++)
            {
                if (item[i].Name == vendingItem.Name)
                {
                    item[i].Quantity = vendingItem.Quantity;
                }
            }
        }
        bool IFoodVendingDataService.RemoveItem(VendingItem vendingItem)
        {
            throw new NotImplementedException();
        }

        string IFoodVendingDataService.SearchItem(string name)
        {
            throw new NotImplementedException();
        }
    }

}
