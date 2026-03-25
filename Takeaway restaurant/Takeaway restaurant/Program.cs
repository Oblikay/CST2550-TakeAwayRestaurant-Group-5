using System;

namespace TakeawayWeb
{
    class Program
    {
        static BinarySearchTree menuTree = new BinarySearchTree();
        static DatabaseHelper database;
        static OrderBST orderTree = new OrderBST();
        static int nextOrderId = 1;

        static void Main(string[] args)
        {
            Console.Write("Enter database file path (or press Enter for default 'restaurant.db'): ");
            string dbPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dbPath))
            {
                dbPath = "restaurant.db";
            }

            database = new DatabaseHelper(dbPath);
            database.InitialiseDatabase();
            database.LoadMenuItems(menuTree);

            // If database was empty, load sample data
            if (menuTree.Count() == 0)
            {
                Console.WriteLine("No data found in database. Loading sample data...");
                LoadSampleData();
                database.SaveAllMenuItems(menuTree);
            }

            bool running = true;
            static void PlaceOrder()
            {
                Console.WriteLine("\n--- Place New Order ---");

                Console.Write("Enter customer name: ");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.Write("Enter phone number: ");
                string phone = Console.ReadLine();

                Console.Write("Enter delivery address: ");
                string address = Console.ReadLine();

                Order order = new Order(nextOrderId, name, phone, address);

                bool addingItems = true;
                while (addingItems)
                {
                    Console.WriteLine("\n--- Current Menu ---\n");
                    menuTree.DisplayAll();

                    Console.Write("\nEnter item ID to add (or 0 to finish): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int itemId))
                    {
                        if (itemId == 0)
                        {
                            addingItems = false;
                        }
                        else
                        {
                            MenuItem item = menuTree.SearchById(itemId);
                            if (item != null)
                            {
                                order.AddItem(item);
                                Console.WriteLine($"Added '{item.Name}' to order. Current total: £{order.TotalPrice:F2}");
                            }
                            else
                            {
                                Console.WriteLine("Item not found. Try again.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }

                if (order.ItemCount > 0)
                {
                    orderTree.Insert(order);
                    Console.WriteLine($"\nOrder #{nextOrderId} placed successfully!");
                    Console.WriteLine(order.ToString());
                    nextOrderId++;
                }
                else
                {
                    Console.WriteLine("Order cancelled - no items added.");
                }
            }

            static void ViewOrders()
            {
                Console.WriteLine("\n--- All Orders ---\n");
                orderTree.DisplayAll();
                Console.WriteLine($"Total orders: {orderTree.Count()}");
            }

            static void UpdateOrderStatus()
            {
                Console.Write("\nEnter Order ID: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int orderId))
                {
                    Order order = orderTree.SearchById(orderId);
                    if (order != null)
                    {
                        Console.WriteLine($"\nCurrent status: {order.Status}");
                        Console.WriteLine("Select new status:");
                        Console.WriteLine("  1. Pending");
                        Console.WriteLine("  2. Preparing");
                        Console.WriteLine("  3. Out for Delivery");
                        Console.WriteLine("  4. Completed");
                        Console.Write("Select: ");
                        string statusChoice = Console.ReadLine();

                        switch (statusChoice)
                        {
                            case "1":
                                order.Status = "Pending";
                                break;
                            case "2":
                                order.Status = "Preparing";
                                break;
                            case "3":
                                order.Status = "Out for Delivery";
                                break;
                            case "4":
                                order.Status = "Completed";
                                break;
                            default:
                                Console.WriteLine("Invalid option.");
                                return;
                        }

                        Console.WriteLine($"Order #{orderId} status updated to: {order.Status}");
                    }
                    else
                    {
                        Console.WriteLine($"No order found with ID: {orderId}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
            while (running)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewMenu();
                        break;
                    case "2":
                        SearchMenuItem();
                        break;
                    case "3":
                        AddMenuItem();
                        break;
                    case "4":
                        RemoveMenuItem();
                        break;
                    case "5":
                        SearchByCategory();
                        break;
                    case "6":
                        PlaceOrder();
                        break;
                    case "7":
                        ViewOrders();
                        break;
                    case "8":
                        UpdateOrderStatus();
                        break;
                    case "9":
                        SaveToDatabase();
                        break;
                    case "0":
                        SaveToDatabase();
                        running = false;
                        Console.WriteLine("\nThank you for using Takeaway Restaurant! Goodbye.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
        }
        static void DisplayMainMenu()
        {
            Console.WriteLine("\n================");
            Console.WriteLine("TAKEAWAY RESTAURANT SYSTEM");
            Console.WriteLine("==================");
            Console.WriteLine("1. View Full Menu");
            Console.WriteLine("2. Search Menu Item");
            Console.WriteLine("3. Add Menu Item");
            Console.WriteLine("4. Remove Menu Item");
            Console.WriteLine("5. Search by Category");
            Console.WriteLine("6. Place Order");
            Console.WriteLine("7. View Orders");
            Console.WriteLine("8. Update Order Status");
            Console.WriteLine("9. Save to Database");
            Console.WriteLine("0. Exit");
            Console.WriteLine("==================");
            Console.Write("Select an option: ");
        }

        static void ViewMenu()
        {
            Console.WriteLine("\n--- Full Menu ---\n");
            menuTree.DisplayAll();
            Console.WriteLine($"Total items: {menuTree.Count()}");
        }

        static void SearchMenuItem()
        {
            Console.WriteLine("\nSearch by:");
            Console.WriteLine("  1. Item ID");
            Console.WriteLine("  2. Item Name");
            Console.Write("Select: ");
            string searchChoice = Console.ReadLine();

            if (searchChoice == "1")
            {
                Console.Write("Enter Item ID: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int id))
                {
                    MenuItem found = menuTree.SearchById(id);
                    if (found != null)
                    {
                        Console.WriteLine("\n--- Item Found ---");
                        Console.WriteLine(found.ToString());
                    }
                    else
                    {
                        Console.WriteLine($"\nNo item found with ID: {id}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
            else if (searchChoice == "2")
            {
                Console.Write("Enter Item Name: ");
                string name = Console.ReadLine();

                MenuItem found = menuTree.SearchByName(name);
                if (found != null)
                {
                    Console.WriteLine("\n--- Item Found ---");
                    Console.WriteLine(found.ToString());
                }
                else
                {
                    Console.WriteLine($"\nNo item found with name: {name}");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }

        static void AddMenuItem()
        {
            Console.WriteLine("\n--- Add New Menu Item ---");

            Console.Write("Enter Item ID: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid ID. Must be a number.");
                return;
            }

            Console.Write("Enter Item Name: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            Console.Write("Enter Category (Starters/Mains/Desserts/Drinks): ");
            string category = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(category))
            {
                Console.WriteLine("Category cannot be empty.");
                return;
            }

            Console.Write("Enter Price: £");
            string priceInput = Console.ReadLine();
            if (!decimal.TryParse(priceInput, out decimal price) || price < 0)
            {
                Console.WriteLine("Invalid price. Must be a positive number.");
                return;
            }

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            MenuItem newItem = new MenuItem(id, name, category, price, description);
            menuTree.Insert(newItem);
            Console.WriteLine($"\n'{name}' has been added to the menu!");
        }

        static void RemoveMenuItem()
        {
            Console.Write("\nEnter the ID of the item to remove: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                MenuItem item = menuTree.SearchById(id);
                if (item != null)
                {
                    Console.WriteLine($"\nRemoving: {item.Name} (£{item.Price:F2})");
                    Console.Write("Are you sure? (y/n): ");
                    string confirm = Console.ReadLine();

                    if (confirm?.ToLower() == "y")
                    {
                        menuTree.Delete(id);
                        Console.WriteLine("Item removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Removal cancelled.");
                    }
                }
                else
                {
                    Console.WriteLine($"No item found with ID: {id}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        static void SearchByCategory()
        {
            Console.Write("\nEnter category (Starters/Mains/Desserts/Drinks): ");
            string category = Console.ReadLine();
            Console.WriteLine($"\n--- {category} ---\n");
            menuTree.DisplayByCategory(category);
        }

        static void LoadSampleData()
        {
            menuTree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy vegetable spring rolls"));
            menuTree.Insert(new MenuItem(5, "Garlic Bread", "Starters", 3.50m, "Toasted bread with garlic butter"));
            menuTree.Insert(new MenuItem(15, "Chicken Tikka Masala", "Mains", 9.99m, "Creamy spiced chicken curry"));
            menuTree.Insert(new MenuItem(8, "Fish and Chips", "Mains", 8.50m, "Battered cod with thick-cut chips"));
            menuTree.Insert(new MenuItem(20, "Margherita Pizza", "Mains", 7.99m, "Classic tomato and mozzarella pizza"));
            menuTree.Insert(new MenuItem(3, "Chocolate Brownie", "Desserts", 4.99m, "Warm fudge brownie with ice cream"));
            menuTree.Insert(new MenuItem(12, "Cola", "Drinks", 1.99m, "330ml can of cola"));
            menuTree.Insert(new MenuItem(18, "Lemonade", "Drinks", 1.99m, "Freshly squeezed lemonade"));
        }
        static void SaveToDatabase()
        {
            database.SaveAllMenuItems(menuTree);
            Console.WriteLine("Data saved successfully");
        }
    }
}