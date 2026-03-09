# CST2550 - Takeaway Restaurant System (Group 5)

## About
A console-based Takeaway Restaurant ordering system built in C# .NET. The application uses a custom Binary Search Tree (BST) data structure to store and manage menu items and orders, with SQLite database integration for persistent storage.

## Team Members
- **Team Leader (Scrum Master):** [Name]
- **Secretary:** [Name]
- **Developer:** Nigel
- **Developer:** [Name]
- **Tester:** [Name]

## Prerequisites
Before running this project, make sure you have the following installed:

1. **Visual Studio 2022** (Community edition is free)
   - Download: https://visualstudio.microsoft.com/downloads/
   - During installation, select the **".NET desktop development"** workload

2. **.NET 8.0 SDK** (usually included with Visual Studio)
   - Download: https://dotnet.microsoft.com/download/dotnet/8.0

3. **Git** (for cloning the repository)
   - Download: https://git-scm.com/download/win

## How to Clone and Run

### Step 1: Clone the Repository
Open a command prompt and run:
```
git clone https://github.com/Oblikay/CST2550-TakeAwayRestaurant-Group-5.git
```

### Step 2: Open in Visual Studio
1. Open Visual Studio 2022
2. Click **"Open a project or solution"**
3. Navigate to the cloned folder and open the `.sln` file (e.g., `Takeaway restaurant.sln`)

### Step 3: Install NuGet Packages
The project uses SQLite for database operations. If the packages don't restore automatically:
1. Go to **Tools → NuGet Package Manager → Manage NuGet Packages for Solution**
2. Click the **"Restore"** button at the top, OR
3. Search for **Microsoft.Data.Sqlite** → Install it for the main project

### Step 4: Build and Run
1. Press **Ctrl + Shift + B** to build the solution
2. Press **F5** to run the application
3. When prompted, press **Enter** to use the default database file (`restaurant.db`) or type a custom file path

## How to Use the Application

When the program starts, you will see a menu with the following options:

| Option | Description |
|--------|-------------|
| 1 | View the full restaurant menu (sorted by ID) |
| 2 | Search for a menu item by ID or name |
| 3 | Add a new menu item to the menu |
| 4 | Remove a menu item by ID |
| 5 | Search menu items by category (Starters/Mains/Desserts/Drinks) |
| 6 | Place a new customer order |
| 7 | View all orders |
| 8 | Update an order's status (Pending/Preparing/Out for Delivery/Completed) |
| 9 | Save current data to the database |
| 0 | Save and exit |

## Running the Unit Tests
1. Open the solution in Visual Studio
2. Go to **Test → Run All Tests** (or press **Ctrl + R, A**)
3. All 12 tests should pass with green ticks

## Project Structure

```
Takeaway restaurant/
├── MenuItem.cs            - Menu item data model
├── BSTNode.cs             - Binary Search Tree node
├── BinarySearchTree.cs    - BST with Insert, Search, Delete, Traversal
├── Order.cs               - Customer order data model
├── OrderBST.cs            - BST for storing orders
├── DatabaseHelper.cs      - SQLite database read/write operations
├── Program.cs             - Main entry point and console menu
├── CreateDatabase.txt     - SQL statements for creating the database
│
TakeAwayRestaurant.Tests/
├── BSTTests.cs            - MSTest unit tests for BST operations
```

## Database
The application uses SQLite for persistent storage. On first run, a `restaurant.db` file is created automatically with sample menu data. The SQL statements used to create the database can be found in `CreateDatabase.txt`.

## Troubleshooting

**"Microsoft.Data.Sqlite not found" error:**
Go to Tools → NuGet Package Manager → Manage NuGet Packages for Solution → Search for Microsoft.Data.Sqlite → Install

**"pipe has ended" error when pushing to Git:**
Make sure the `.gitignore` file is in place to exclude `.vs/`, `bin/`, and `obj/` folders. Go to View → Git Changes, unstage any `.vs` files, then try again.

**Build errors about decimal/string conversion:**
Make sure `Price` in MenuItem.cs is declared as `public decimal Price { get; set; }` not `public string Price`.
