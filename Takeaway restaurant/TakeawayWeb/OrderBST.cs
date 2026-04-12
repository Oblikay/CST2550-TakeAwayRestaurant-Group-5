using System;

namespace TakeawayWeb
{
    public class OrderNode
    {
        public Order Data { get; set; }
        public OrderNode Left { get; set; }
        public OrderNode Right { get; set; }

        public OrderNode(Order data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    public class OrderBST
    {
        private OrderNode root;

        public OrderBST()
        {
            root = null;
        }

        public void Insert(Order order)
        {
            root = InsertRecursive(root, order);
        }

        private OrderNode InsertRecursive(OrderNode node, Order order)
        {
            if (node == null)
            {
                return new OrderNode(order);
            }

            if (order.OrderId < node.Data.OrderId)
            {
                node.Left = InsertRecursive(node.Left, order);
            }
            else if (order.OrderId > node.Data.OrderId)
            {
                node.Right = InsertRecursive(node.Right, order);
            }
            else
            {
                node.Data = order;
            }

            return node;
        }

        public Order SearchById(int orderId)
        {
            OrderNode result = SearchRecursive(root, orderId);
            return result != null ? result.Data : null;
        }

        private OrderNode SearchRecursive(OrderNode node, int orderId)
        {
            if (node == null || node.Data.OrderId == orderId)
            {
                return node;
            }

            if (orderId < node.Data.OrderId)
            {
                return SearchRecursive(node.Left, orderId);
            }

            return SearchRecursive(node.Right, orderId);
        }

        public void DisplayAll()
        {
            if (root == null)
            {
                Console.WriteLine("No orders found.");
                return;
            }
            InOrderTraversal(root);
        }

        private void InOrderTraversal(OrderNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.WriteLine(node.Data.ToString());
                Console.WriteLine();
                InOrderTraversal(node.Right);
            }
        }
        public Order[] GetAllOrders()
        {
            Order[] orders = new Order[Count()];
            int index = 0;
            GetAllOrdersRecursive(root, orders, ref index);
            return orders;
        }

        private void GetAllOrdersRecursive(OrderNode node, Order[] orders, ref int index)
        {
            if (node != null)
            {
                GetAllOrdersRecursive(node.Left, orders, ref index);
                orders[index] = node.Data;
                index++;
                GetAllOrdersRecursive(node.Right, orders, ref index);
            }
        }

        public int Count()
        {
            return CountRecursive(root);
        }

        private int CountRecursive(OrderNode node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }
    }
}