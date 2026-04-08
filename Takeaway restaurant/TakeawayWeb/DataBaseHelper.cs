using System;
using Microsoft.Data.Sqlite;

namespace TakeawayWeb
{
    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper(string databasePath)
        {
            connectionString = $"Data Source={databasePath}";
        }

        //  MENU ITEMS TABLE 
        public void InitialiseDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS MenuItems (
                        Id INTEGER PRIMARY KEY,
                        Name TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Price REAL NOT NULL,
                        Description TEXT
                    );";

                using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void SaveMenuItem(MenuItem item)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string insertSql = @"
                    INSERT OR REPLACE INTO MenuItems (Id, Name, Category, Price, Description)
                    VALUES (@Id, @Name, @Category, @Price, @Description);";

                using (SqliteCommand command = new SqliteCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Category", item.Category);
                    command.Parameters.AddWithValue("@Price", (double)item.Price);
                    command.Parameters.AddWithValue("@Description", item.Description ?? "");
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void SaveAllMenuItems(BinarySearchTree tree)
        {
            MenuItem[] items = tree.GetAllItems();

            ClearMenuItems();

            for (int i = 0; i < items.Length; i++)
            {
                SaveMenuItem(items[i]);
            }

            Console.WriteLine($"{items.Length} items saved to database.");
        }

        public void LoadMenuItems(BinarySearchTree tree)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string selectSql = "SELECT Id, Name, Category, Price, Description FROM MenuItems;";

                using (SqliteCommand command = new SqliteCommand(selectSql, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        int count = 0;

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string category = reader.GetString(2);
                            decimal price = (decimal)reader.GetDouble(3);
                            string description = reader.GetString(4);

                            MenuItem item = new MenuItem(id, name, category, price, description);
                            tree.Insert(item);
                            count++;
                        }

                        Console.WriteLine($"{count} items loaded from database.");
                    }
                }

                connection.Close();
            }
        }
        public void DeleteMenuItem(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string deleteSql = "DELETE FROM MenuItems WHERE Id = @Id;";

                using (SqliteCommand command = new SqliteCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void ClearMenuItems()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string deleteSql = "DELETE FROM MenuItems;";

                using (SqliteCommand command = new SqliteCommand(deleteSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        // ==================== ORDERS TABLE ====================

        public void InitialiseOrdersTable()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS Orders (
                        OrderId INTEGER PRIMARY KEY,
                        CustomerName TEXT NOT NULL,
                        CustomerPhone TEXT,
                        CustomerAddress TEXT,
                        ItemIds TEXT,
                        ItemNames TEXT,
                        ItemPrices TEXT,
                        ItemCount INTEGER,
                        TotalPrice REAL,
                        Status TEXT,
                        OrderDate TEXT
                    );";

                using (SqliteCommand command = new SqliteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void SaveOrder(Order order)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string ids = "";
                string names = "";
                string prices = "";
                for (int i = 0; i < order.ItemCount; i++)
                {
                    if (i > 0) { ids += "|"; names += "|"; prices += "|"; }
                    ids += order.ItemIds[i].ToString();
                    names += order.ItemNames[i];
                    prices += order.ItemPrices[i].ToString();
                }

                string sql = @"
                    INSERT OR REPLACE INTO Orders 
                    (OrderId, CustomerName, CustomerPhone, CustomerAddress, ItemIds, ItemNames, ItemPrices, ItemCount, TotalPrice, Status, OrderDate)
                    VALUES (@OrderId, @Name, @Phone, @Address, @Ids, @Names, @Prices, @Count, @Total, @Status, @Date);";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", order.OrderId);
                    command.Parameters.AddWithValue("@Name", order.CustomerName);
                    command.Parameters.AddWithValue("@Phone", order.CustomerPhone ?? "");
                    command.Parameters.AddWithValue("@Address", order.CustomerAddress ?? "");
                    command.Parameters.AddWithValue("@Ids", ids);
                    command.Parameters.AddWithValue("@Names", names);
                    command.Parameters.AddWithValue("@Prices", prices);
                    command.Parameters.AddWithValue("@Count", order.ItemCount);
                    command.Parameters.AddWithValue("@Total", (double)order.TotalPrice);
                    command.Parameters.AddWithValue("@Status", order.Status);
                    command.Parameters.AddWithValue("@Date", order.OrderDate.ToString("o"));
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void LoadOrders(OrderBST orderTree)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM Orders;";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        int count = 0;

                        while (reader.Read())
                        {
                            int orderId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string phone = reader.GetString(2);
                            string address = reader.GetString(3);
                            string itemIdsStr = reader.GetString(4);
                            string itemNamesStr = reader.GetString(5);
                            string itemPricesStr = reader.GetString(6);
                            int itemCount = reader.GetInt32(7);
                            decimal totalPrice = (decimal)reader.GetDouble(8);
                            string status = reader.GetString(9);
                            string dateStr = reader.GetString(10);

                            Order order = new Order(orderId, name, phone, address);
                            order.Status = status;
                            order.TotalPrice = totalPrice;
                            order.ItemCount = itemCount;
                            order.OrderDate = DateTime.Parse(dateStr);

                            if (!string.IsNullOrEmpty(itemIdsStr))
                            {
                                string[] idsArr = itemIdsStr.Split('|');
                                string[] namesArr = itemNamesStr.Split('|');
                                string[] pricesArr = itemPricesStr.Split('|');

                                for (int i = 0; i < itemCount; i++)
                                {
                                    order.ItemIds[i] = int.Parse(idsArr[i]);
                                    order.ItemNames[i] = namesArr[i];
                                    order.ItemPrices[i] = decimal.Parse(pricesArr[i]);
                                }
                            }

                            orderTree.Insert(order);
                            count++;
                        }

                        Console.WriteLine($"{count} orders loaded from database.");
                    }
                }

                connection.Close();
            }
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sql = "UPDATE Orders SET Status = @Status WHERE OrderId = @OrderId;";

                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}