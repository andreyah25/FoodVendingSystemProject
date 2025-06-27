<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using VendingCommon;
using FoodVendingData;

namespace VendingDataService
{
    public class DBFoodVendingDataService : IFoodVendingDataService
    {
        private static readonly string connectionString =
"           Data Source=Andrea\\SQLEXPRESS;Initial Catalog=VendingMachineDB;Integrated Security=True;TrustServerCertificate=True;";

        public List<SnackItem> LoadItems() => GetAllItems();

        public List<SnackItem> GetAllItems()
        {
            var snacks = new List<SnackItem>();
            var query = "SELECT * FROM FoodInventory";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        snacks.Add(new SnackItem
                        {
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        });
                    }
                }
            }
            return snacks;
        }

        public SnackItem GetItemByName(string name)
        {
            SnackItem item = null;
            string query = "SELECT * FROM FoodInventory WHERE Name = @Name";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new SnackItem
                        {
                            
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };
                    }
                }
            }
            return item;
        }

        public bool AddItem(SnackItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name) || item.Price <= 0 || item.Quantity < 0)
                return false;

            string insert = "INSERT INTO FoodInventory (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool RemoveItem(string name)
        {
            string delete = "DELETE FROM FoodInventory WHERE Name = @Name";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateItemQuantity(string name, int deltaQuantity)
        {
            var item = GetItemByName(name);
            if (item == null) return false;

            int newQty = item.Quantity + deltaQuantity;
            if (newQty < 0) return false;

            string update = "UPDATE FoodInventory SET Quantity = @Quantity WHERE Name = @Name";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@Quantity", newQty);
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AddNewItem(SnackItem item)
        {
            return AddItem(item);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using VendingCommon;
using FoodVendingData;

namespace VendingDataService
{
    public class DBFoodVendingDataService : IFoodVendingDataService
    {
        private static readonly string connectionString =
"           Data Source=Andrea\\SQLEXPRESS;Initial Catalog=VendingMachineDB;Integrated Security=True;TrustServerCertificate=True;";

        public List<SnackItem> LoadItems() => GetAllItems();

        public List<SnackItem> GetAllItems()
        {
            var snacks = new List<SnackItem>();
            var query = "SELECT * FROM FoodInventory";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        snacks.Add(new SnackItem
                        {
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        });
                    }
                }
            }
            return snacks;
        }

        public SnackItem GetItemByName(string name)
        {
            SnackItem item = null;
            string query = "SELECT * FROM FoodInventory WHERE Name = @Name";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new SnackItem
                        {
                            
                            Name = reader["Name"].ToString(),
                            Price = Convert.ToDouble(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };
                    }
                }
            }
            return item;
        }

        public bool AddItem(SnackItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Name) || item.Price <= 0 || item.Quantity < 0)
                return false;

            string insert = "INSERT INTO FoodInventory (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(insert, conn))
            {
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool RemoveItem(string name)
        {
            string delete = "DELETE FROM FoodInventory WHERE Name = @Name";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(delete, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateItemQuantity(string name, int deltaQuantity)
        {
            var item = GetItemByName(name);
            if (item == null) return false;

            int newQty = item.Quantity + deltaQuantity;
            if (newQty < 0) return false;

            string update = "UPDATE FoodInventory SET Quantity = @Quantity WHERE Name = @Name";

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@Quantity", newQty);
                cmd.Parameters.AddWithValue("@Name", name);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AddNewItem(SnackItem item)
        {
            return AddItem(item);
        }
    }
}
>>>>>>> 1bf1ccf10240483bb6f0ffc9c613fb156e742f61
