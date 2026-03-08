using System;

namespace Takeaway_restaurant
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int[] ItemIds { get; set; }
        public string[] ItemNames { get; set; }
        public decimal[] ItemPrices { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

        public Order(int orderId, string customerName, string customerPhone, string customerAddress)
        {
            OrderId = orderId;
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            CustomerAddress = customerAddress;
            ItemIds = new int[50];
            ItemNames = new string[50];
            ItemPrices = new decimal[50];
            ItemCount = 0;
            TotalPrice = 0;
            Status = "Pending";
            OrderDate = DateTime.Now;
        }

        public void AddItem(MenuItem item)
        {
            if (ItemCount < 50)
            {
                ItemIds[ItemCount] = item.Id;
                ItemNames[ItemCount] = item.Name;
                ItemPrices[ItemCount] = item.Price;
                TotalPrice += item.Price;
                ItemCount++;
            }
            else
            {
                Console.WriteLine("Order is full. Cannot add more items.");
            }
        }

        public override string ToString()
        {
            string output = $"Order #{OrderId} - {CustomerName} ({Status})\n";
            output += $"  Phone: {CustomerPhone}\n";
            output += $"  Address: {CustomerAddress}\n";
            output += $"  Date: {OrderDate:dd/MM/yyyy HH:mm}\n";
            output += "  Items:\n";

            for (int i = 0; i < ItemCount; i++)
            {
                output += $"    - {ItemNames[i]} (£{ItemPrices[i]:F2})\n";
            }

            output += $"  Total: £{TotalPrice:F2}";
            return output;
        }
    }
}