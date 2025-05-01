using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class Order
    {
        private Dictionary<string, int> orderItems;

        public Order()
        {
            orderItems = new Dictionary<string, int>();
        }

        public void AddItem(string itemName, int quantity)
        {
            if (orderItems.ContainsKey(itemName))
            {
                orderItems[itemName] += quantity;
            }
            else
            {
                orderItems[itemName] = quantity;
            }
        }

        
    }
}