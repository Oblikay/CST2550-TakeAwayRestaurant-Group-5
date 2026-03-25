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

if (menuTree.Count() == 0)
{
    menuTree.Insert(new MenuItem(10, "Spring Rolls", "Starters", 4.50m, "Crispy vegetable spring rolls"));
    menuTree.Insert(new MenuItem(5, "Garlic Bread", "Starters", 3.50m, "Toasted bread with garlic butter"));
    menuTree.Insert(new MenuItem(15, "Chicken Tikka Masala", "Mains", 9.99m, "Creamy spiced chicken curry"));
    menuTree.Insert(new MenuItem(8, "Fish and Chips", "Mains", 8.50m, "Battered cod with thick-cut chips"));
    menuTree.Insert(new MenuItem(20, "Margherita Pizza", "Mains", 7.99m, "Classic tomato and mozzarella pizza"));
    menuTree.Insert(new MenuItem(3, "Chocolate Brownie", "Desserts", 4.99m, "Warm fudge brownie with ice cream"));
    menuTree.Insert(new MenuItem(12, "Cola", "Drinks", 1.99m, "330ml can of cola"));
    menuTree.Insert(new MenuItem(18, "Lemonade", "Drinks", 1.99m, "Freshly squeezed lemonade"));
    dbHelper.SaveAllMenuItems(menuTree);
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();