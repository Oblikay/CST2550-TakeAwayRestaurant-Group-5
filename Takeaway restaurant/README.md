# CST2550 – Takeaway Restaurant Management System
### Group 5 | Middlesex University Mauritius

---

## How to Run the Application

1. Open Visual Studio
2. Navigate to `Takeaway restaurant/TakeawayWeb/`
3. Open `TakeawayWeb.csproj`
4. Press **F5** or click **Run** to start the web application
5. When prompted, enter the database file path (or press Enter to use the default `restaurant.db`)
6. The app will open in your browser — log in as Admin or Customer to use the system

> **Default login credentials:**
> - Admin: username `admin`, password `admin`
> - Customer: username `customer`, password `customer`
> *(Update these if your actual credentials differ)*

---

## Repository Structure

```
CST2550-TakeAwayRestaurant-Group-5/
│
├── Takeaway restaurant/                  ← Main project folder
│   │
│   ├── TakeawayWeb/                      ← WEB APPLICATION (run this)
│   │   ├── Program.cs                    ← App entry point — starts the web server
│   │   ├── AIAgent.cs                    ← AI chat agent logic
│   │   ├── BinarySearchTree.cs           ← Custom BST data structure
│   │   ├── BSTNode.cs                    ← BST node class
│   │   ├── MenuItem.cs                   ← MenuItem data class
│   │   ├── Order.cs                      ← Order data class
│   │   ├── OrderBST.cs                   ← BST for storing orders
│   │   ├── DataBaseHelper.cs             ← SQLite read/write logic
│   │   ├── Controllers/
│   │   │   ├── AccountController.cs      ← Login and logout
│   │   │   ├── AdminController.cs        ← Admin: menu management, orders
│   │   │   ├── CustomerController.cs     ← Customer: browse, search, order
│   │   │   ├── AIController.cs           ← AI chat endpoint (POST)
│   │   │   └── HomeController.cs         ← Home page routing
│   │   ├── Views/                        ← HTML pages for each controller
│   │   │   ├── Account/Login.cshtml      ← Login page
│   │   │   ├── Admin/                    ← Admin pages (dashboard, menu, orders)
│   │   │   └── Customer/                 ← Customer pages (menu, order, AI chat)
│   │   └── TakeawayWeb.csproj            ← Web app project file
│   │
│   ├── Takeaway restaurant/              ← ORIGINAL CONSOLE APPLICATION
│   │   ├── Program.cs                    ← Console app entry point
│   │   ├── BinarySearchTree.cs           ← BST implementation (console version)
│   │   ├── BSTNode.cs                    ← BST node class
│   │   ├── MenuItem.cs                   ← MenuItem class
│   │   ├── Order.cs                      ← Order class
│   │   ├── OrderBST.cs                   ← Order BST
│   │   ├── DataBaseHelper.cs             ← Database helper
│   │   └── CreateDatabase.txt            ← SQL statements to create the database
│   │
│   └── TakeAayRestaurant.tests/          ← UNIT TESTS (run these in Test Explorer)
│       ├── BSTTests.cs                   ← All 12 MSTest unit tests for the BST
│       ├── MSTestSettings.cs             ← Test configuration
│       └── TakeAayRestaurant.tests.csproj← Test project file
│
├── screenshots/                          ← Diagrams and flowcharts
│   ├── Flowcharts/
│   │   ├── Flowchart.png                 ← Overall system flowchart
│   │   ├── Login.png                     ← Login flowchart
│   │   ├── Admin/                        ← Admin process flowcharts
│   │   └── User/                         ← Customer process flowcharts
│   └── errors.png                        ← Error handling screenshot
│
├── Use_case_diagram.png                  ← System use case diagram
├── README.md                             ← This file
└── Resturaunt_login.slnf                 ← Solution filter file
```

---

## Key Files Explained

| File | What it does |
|---|---|
| `TakeawayWeb/Program.cs` | Starts the web application and registers services |
| `TakeawayWeb/AIAgent.cs` | Rule-based AI chat agent — queries the live BST to answer customer questions |
| `TakeawayWeb/BinarySearchTree.cs` | Custom BST — insert, delete, search, traversal algorithms |
| `TakeawayWeb/DataBaseHelper.cs` | Reads and writes menu/order data to the SQLite database |
| `TakeawayWeb/Controllers/AdminController.cs` | Handles all admin actions — menu management and order updates |
| `TakeawayWeb/Controllers/CustomerController.cs` | Handles all customer actions — browsing, searching, ordering |
| `TakeAayRestaurant.tests/BSTTests.cs` | 12 unit tests covering BST insert, delete, search, and traversal |
| `Takeaway restaurant/CreateDatabase.txt` | SQL CREATE and INSERT statements for the database |

---

## How to Run the Unit Tests

1. Open the solution in Visual Studio
2. Go to **Test → Test Explorer**
3. Click **Run All Tests**
4. All 12 tests should pass

---

## How to Compile

**Requirements:**
- .NET 8.0 SDK or later
- Visual Studio 2022 (or later)
- No additional setup needed — SQLite is included via NuGet

**Steps:**
1. Clone or download the repository
2. Open `Takeaway restaurant/Takeaway restaurant.sln` in Visual Studio
3. Right-click the solution → **Restore NuGet Packages**
4. Set `TakeawayWeb` as the startup project
5. Press **F5** to build and run
