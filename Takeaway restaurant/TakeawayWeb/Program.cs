using TakeawayWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<BinarySearchTree>();
builder.Services.AddSingleton<OrderBST>();

var app = builder.Build();

var menuTree = app.Services.GetRequiredService<BinarySearchTree>();
var dbHelper = new DatabaseHelper("restaurant.db");
dbHelper.InitialiseDatabase();
dbHelper.LoadMenuItems(menuTree);

dbHelper.InitialiseOrdersTable();
var orderTree = app.Services.GetRequiredService<OrderBST>();
dbHelper.LoadOrders(orderTree);


if (menuTree.Count() == 0)
{
    // Starters
    menuTree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy vegetable spring rolls"));
    menuTree.Insert(new MenuItem(5, "Garlic Bread", "Starters", 3.50m, "Toasted bread with garlic butter"));
    menuTree.Insert(new MenuItem(2, "Chicken Wings", "Starters", 5.99m, "Spicy buffalo chicken wings"));
    menuTree.Insert(new MenuItem(7, "Soup of the Day", "Starters", 3.99m, "Freshly made daily soup"));

    // Mains
    menuTree.Insert(new MenuItem(15, "Chicken Tikka Masala", "Mains", 9.99m, "Creamy spiced chicken curry"));
    menuTree.Insert(new MenuItem(8, "Fish and Chips", "Mains", 8.50m, "Battered cod with thick-cut chips"));
    menuTree.Insert(new MenuItem(20, "Margherita Pizza", "Mains", 7.99m, "Classic tomato and mozzarella pizza"));
    menuTree.Insert(new MenuItem(22, "Beef Burger", "Mains", 8.99m, "Angus beef with lettuce and tomato"));
    menuTree.Insert(new MenuItem(25, "Lamb Kebab", "Mains", 10.50m, "Grilled lamb with rice and salad"));
    menuTree.Insert(new MenuItem(28, "Pepperoni Pizza", "Mains", 8.99m, "Loaded with pepperoni and cheese"));
    menuTree.Insert(new MenuItem(30, "Chicken Fried Rice", "Mains", 7.50m, "Stir-fried rice with chicken and vegetables"));

    // Desserts
    menuTree.Insert(new MenuItem(3, "Chocolate Brownie", "Desserts", 4.99m, "Warm fudge brownie with ice cream"));
    menuTree.Insert(new MenuItem(35, "Cheesecake", "Desserts", 5.50m, "New York style baked cheesecake"));
    menuTree.Insert(new MenuItem(37, "Ice Cream Sundae", "Desserts", 4.50m, "Three scoops with chocolate sauce"));

    // Drinks
    menuTree.Insert(new MenuItem(12, "Cola", "Drinks", 1.99m, "330ml can of cola"));
    menuTree.Insert(new MenuItem(18, "Lemonade", "Drinks", 1.99m, "Freshly squeezed lemonade"));
    menuTree.Insert(new MenuItem(40, "Orange Juice", "Drinks", 2.50m, "Freshly pressed orange juice"));
    menuTree.Insert(new MenuItem(42, "Water", "Drinks", 0.99m, "500ml bottled water"));
    menuTree.Insert(new MenuItem(44, "Milkshake", "Drinks", 3.99m, "Chocolate, vanilla, or strawberry"));
    dbHelper.SaveAllMenuItems(menuTree);
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();