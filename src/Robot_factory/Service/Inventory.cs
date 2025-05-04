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
                    string pieceName = piece.Key;
                    int pieceQuantity = piece.Value * quantity;
                    
                    Console.WriteLine($" {pieceQuantity} {pieceName}");

                    if (robotParts.ContainsKey(piece.Key))
                    {
                        robotParts[pieceName] += pieceQuantity;
                    }
                    else
                    {
                        robotParts[pieceName] = pieceQuantity;
                    }
                }
            }

            Console.WriteLine("\nTotal : \n");
            foreach (var part in robotParts)
            {
                Console.WriteLine($" {part.Value} {part.Key}");
            }
        }

        public bool HasItemInQuantity(string itemName, int quantity)
        {
            if (inventory.ContainsKey(itemName))
            {
                return inventory[itemName] >= quantity;
            }
            return false;
        }

        public void RemovePieceInStock(Dictionary<string, int> robotQuantities)
        {
            foreach (var robot in robotQuantities)
            {
                string robotName = robot.Key;
                int quantity = robot.Value;

                Dictionary<string, int> pieces = robotName switch
                    {
                        "RobotI" => new RobotI().GetPieces(),
                        "RobotII" => new RobotII().GetPieces(),
                        "RobotIII" => new RobotIII().GetPieces(),
                        _ => new Dictionary<string, int>()
                    };

                foreach (var piece in pieces)
                {
                    string pieceName = piece.Key;
                    int pieceQuantity = piece.Value * quantity;

                    if (inventory.ContainsKey(pieceName))
                    {
                        inventory[pieceName] -= pieceQuantity;
                        
                        if (inventory[pieceName] <= 0)
                        {
                            inventory.Remove(pieceName);
                        }
                    }
                }
            }
        }
    }
}