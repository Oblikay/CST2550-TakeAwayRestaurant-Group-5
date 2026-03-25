using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeawayWeb
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category {  get; set; }
        public decimal Price {  get; set; }
        public string Description { get; set; }

        public MenuItem(int id, string name, string category, decimal price, string description)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;           
            Description = description; 
        }
        public override string ToString()
        {
            return $"[{Id}] { Name} ({ Category}) - ${ Price: F2}\n {Description}";
        }
    }
}
