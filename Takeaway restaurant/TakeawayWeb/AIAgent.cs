using System;

namespace TakeawayWeb
{
    public class AIAgent
    {
        private BinarySearchTree menuTree;
        private OrderBST orderTree;

        public AIAgent(BinarySearchTree menu, OrderBST orders)
        {
            menuTree = menu;
            orderTree = orders;
        }

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Please type something! I'm here to help.";
            }

            string lower = input.ToLower().Trim();

            if (ContainsAny(lower, "hello", "hi", "hey", "greetings"))
            {
                return "Hello! Welcome to our Takeaway Restaurant. How can I help you today?";
            }
            else if (ContainsAny(lower, "menu", "show menu", "full menu", "what do you have", "what do you sell"))
            {
                return FormatMenuItems(menuTree.GetAllItems());
            }
            else if (ContainsAny(lower, "starter", "appetiser", "appetizer"))
            {
                return FormatCategory("Starters");
            }
            else if (ContainsAny(lower, "main", "entree", "dinner", "lunch"))
            {
                return FormatCategory("Mains");
            }
            else if (ContainsAny(lower, "dessert", "sweet", "pudding"))
            {
                return FormatCategory("Desserts");
            }
            else if (ContainsAny(lower, "drink", "beverage", "thirsty"))
            {
                return FormatCategory("Drinks");
            }
            else if (ContainsAny(lower, "recommend", "suggest", "what should i", "popular", "best"))
            {
                return HandleRecommendation();
            }
            else if (ContainsAny(lower, "cheap", "budget", "affordable", "lowest price", "bargain"))
            {
                return HandleCheapest();
            }
            else if (ContainsAny(lower, "expensive", "premium", "luxury", "priciest"))
            {
                return HandleMostExpensive();
            }
            else if (ContainsAny(lower, "find", "search", "look for", "do you have"))
            {
                return HandleSearch(lower);
            }
            else if (ContainsAny(lower, "price", "cost", "how much"))
            {
                return HandlePriceQuery(lower);
            }
            else if (ContainsAny(lower, "order", "buy", "purchase", "i want", "i'd like"))
            {
                return "To place an order, head to the <a href='/Customer/Order'>Place Order</a> page where you can select items and enter your delivery details!";
            }
            else if (ContainsAny(lower, "my order", "track", "status"))
            {
                return "You can check your orders on the <a href='/Customer/MyOrders'>My Orders</a> page!";
            }
            else if (ContainsAny(lower, "how many", "count", "total items"))
            {
                return $"We currently have {menuTree.Count()} items on our menu.";
            }
            else if (ContainsAny(lower, "help", "what can you do", "options"))
            {
                return "I can help you with:\n" +
                       "- Browsing the menu or categories (try 'show me starters')\n" +
                       "- Searching for items (try 'find chicken')\n" +
                       "- Checking prices (try 'how much is pizza')\n" +
                       "- Getting recommendations (try 'recommend something')\n" +
                       "- Placing orders (try 'I want to order')\n" +
                       "Just type what you need!";
            }
            else if (ContainsAny(lower, "thank", "thanks", "cheers"))
            {
                return "You're welcome! Is there anything else I can help with?";
            }
            else
            {
                return "I'm not sure I understand that. Try asking about our menu, searching for food, or type 'help' to see what I can do.";
            }
        }

        private string FormatMenuItems(MenuItem[] items)
        {
            if (items.Length == 0) return "The menu is currently empty.";

            string result = "Here's our menu:\n\n";
            for (int i = 0; i < items.Length; i++)
            {
                result += $"[{items[i].Id}] {items[i].Name} ({items[i].Category}) - £{items[i].Price:F2}\n";
                result += $"    {items[i].Description}\n\n";
            }
            return result;
        }

        private string FormatCategory(string category)
        {
            MenuItem[] allItems = menuTree.GetAllItems();
            string result = $"Here are our {category}:\n\n";
            bool found = false;

            for (int i = 0; i < allItems.Length; i++)
            {
                if (allItems[i].Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    result += $"[{allItems[i].Id}] {allItems[i].Name} - £{allItems[i].Price:F2}\n";
                    result += $"    {allItems[i].Description}\n\n";
                    found = true;
                }
            }

            if (!found) return $"No items found in {category}.";
            return result;
        }

        private string HandleRecommendation()
        {
            MenuItem[] items = menuTree.GetAllItems();
            if (items.Length == 0) return "Sorry, the menu is empty right now.";

            Random random = new Random();
            int index = random.Next(items.Length);
            MenuItem pick = items[index];

            return $"I'd recommend trying:\n\n" +
                   $"{pick.Name} ({pick.Category}) - £{pick.Price:F2}\n" +
                   $"{pick.Description}\n\n" +
                   $"Head to <a href='/Customer/Order'>Place Order</a> to get it!";
        }

        private string HandleCheapest()
        {
            MenuItem[] items = menuTree.GetAllItems();
            if (items.Length == 0) return "The menu is empty right now.";

            MenuItem cheapest = items[0];
            for (int i = 1; i < items.Length; i++)
            {
                if (items[i].Price < cheapest.Price)
                    cheapest = items[i];
            }

            return $"Our best value item is:\n{cheapest.Name} - just £{cheapest.Price:F2}!\n{cheapest.Description}";
        }

        private string HandleMostExpensive()
        {
            MenuItem[] items = menuTree.GetAllItems();
            if (items.Length == 0) return "The menu is empty right now.";

            MenuItem expensive = items[0];
            for (int i = 1; i < items.Length; i++)
            {
                if (items[i].Price > expensive.Price)
                    expensive = items[i];
            }

            return $"Our premium item is:\n{expensive.Name} - £{expensive.Price:F2}\n{expensive.Description}";
        }

        private string HandleSearch(string input)
        {
            string[] words = input.Split(' ');
            MenuItem[] items = menuTree.GetAllItems();
            string result = "";
            bool found = false;

            string[] skipWords = { "find", "search", "look", "for", "where", "is", "do", "you", "have", "the", "a" };

            for (int w = 0; w < words.Length; w++)
            {
                bool skip = false;
                for (int s = 0; s < skipWords.Length; s++)
                {
                    if (words[w] == skipWords[s]) { skip = true; break; }
                }
                if (skip) continue;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Name.ToLower().Contains(words[w]) ||
                        items[i].Description.ToLower().Contains(words[w]))
                    {
                        if (!found) result = "I found these items:\n\n";
                        result += $"[{items[i].Id}] {items[i].Name} ({items[i].Category}) - £{items[i].Price:F2}\n";
                        result += $"    {items[i].Description}\n\n";
                        found = true;
                    }
                }
            }

            if (!found) return "Sorry, I couldn't find anything matching that. Try a different search term.";
            return result;
        }

        private string HandlePriceQuery(string input)
        {
            MenuItem[] items = menuTree.GetAllItems();
            string[] words = input.Split(' ');
            string[] skipWords = { "price", "cost", "how", "much", "is", "the", "of", "a" };
            string result = "";
            bool found = false;

            for (int w = 0; w < words.Length; w++)
            {
                bool skip = false;
                for (int s = 0; s < skipWords.Length; s++)
                {
                    if (words[w] == skipWords[s]) { skip = true; break; }
                }
                if (skip) continue;

                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Name.ToLower().Contains(words[w]))
                    {
                        result += $"{items[i].Name} costs £{items[i].Price:F2}\n";
                        found = true;
                    }
                }
            }

            if (!found) return "I couldn't find that item. Try checking our menu first!";
            return result;
        }

        private bool ContainsAny(string input, params string[] keywords)
        {
            for (int i = 0; i < keywords.Length; i++)
            {
                if (input.Contains(keywords[i])) return true;
            }
            return false;
        }
    }
}