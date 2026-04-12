using System;
using Microsoft.Data.Sqlite;

namespace TakeawayWeb
{
    public class DatabaseHelper
    {
        private string connectionString;

        // Constructor takes the database file path
        // This means the user can specify the file name (required by brief)
        public DatabaseHelper(string databasePath)
        {
            connectionString = $"Data Source={databasePath}";
        }

        // Creates the MenuItems table if it doesn't exist
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

        // Saves a single MenuItem to the database
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

        // Saves all items from the BST to the database
        public void SaveAllMenuItems(BinarySearchTree tree)
        {
            MenuItem[] items = tree.GetAllItems();

            // Clear existing data first
            ClearMenuItems();

            for (int i = 0; i < items.Length; i++)
            {
                SaveMenuItem(items[i]);
            }

            Console.WriteLine($"{items.Length} items saved to database.");
        }

        // Loads all items from database into the BST
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

        // Deletes a menu item from the database by Id
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

        // Clears all menu items from the database
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
    }
}