using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class Inventory
    {
        private Dictionary<string, int> inventory;

        public Inventory()
        {
            inventory = new Dictionary<string, int>();
        }

        public void AddItem(string itemName, int quantity)
        {
            if (inventory.ContainsKey(itemName))
            {
                inventory[itemName] += quantity;
            }
            else
            {
                inventory[itemName] = quantity;
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Current Inventory:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Value} {item.Key}");
            }
        }

        public void GetNeededStocks(Dictionary<string, int> robotQuantities)
        {
            var robotI = new RobotI();
            var robotII = new RobotII();
            var robotIII = new RobotIII();

            Dictionary<string, int> robotParts = new Dictionary<string, int>();

            foreach (var robot in robotQuantities)
            {
                string robotName = robot.Key;
                int quantity = robot.Value;

                Dictionary<string,int> pieces = robotName switch
                    {
                        "RobotI" => robotI.GetPieces(),
                        "RobotII" => robotII.GetPieces(),
                        "RobotIII" => robotIII.GetPieces(),
                        _ => new Dictionary<string,int>()
                    };

                Console.WriteLine($"\n{quantity} {robotName} : \n");

                foreach (var piece in pieces)
                {
                    Console.WriteLine($" {piece.Value} {piece.Key}");

                    if (robotParts.ContainsKey(piece.Key))
                    {
                        robotParts[piece.Key] += piece.Value;
                    }
                    else
                    {
                        robotParts[piece.Key] = piece.Value;
                    }
                }
            }

            Console.WriteLine("\nTotal : \n");
            foreach (var part in robotParts)
            {
                Console.WriteLine($" {part.Value} {part.Key}");
            }
        }

        public bool HasItem(string itemName, int quantity)
        {
            if (inventory.ContainsKey(itemName))
            {
                return inventory[itemName] >= quantity;
            }
            return false;
        }
    }
}