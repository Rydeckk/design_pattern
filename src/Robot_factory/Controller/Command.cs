using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using src.Robot_factory.Service;

namespace src.Robot_factory.Controller
{
    public class Command
    {
        private Inventory inventory;
        private List<string> validRobots = new List<string> { "RobotI", "RobotII", "RobotIII" };

        public Command(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute(string command, string[] args)
        {
            var robotQuantities = new Dictionary<string, int>();

            if (command.ToUpper() != "STOCKS")
            {
                try
                {
                    robotQuantities = ProcessArgs(args);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR {ex.Message}");
                    return;
                }
            }

            switch (command.ToUpper())
            {
                case "STOCKS":
                    ProcessStocks();
                    break;
                case "NEEDED_STOCKS":
                    ProcessNeededStocks(robotQuantities);
                    break;
                case "INSTRUCTIONS":
                    ProcessInstructions(robotQuantities);
                    break;
                case "VERIFY":
                    ProcessVerify(robotQuantities);
                    break;
                case "PRODUCE":
                    ProcessProduce(robotQuantities);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        private void ProcessStocks()
        {
            this.inventory.DisplayInventory();
        }

        private Dictionary<string, int> ProcessArgs(string[] args)
        {
            Dictionary<string, int> robotQuantities = new Dictionary<string, int>();

            foreach (var arg in args)
            {
                // Chaque argument est sous la forme "2 RobotI"
                var parts = arg.Trim().Split(' ');
                if (parts.Length != 2)
                {
                    throw new ArgumentException($"Invalid argument format : {arg}");
                }

                if (!int.TryParse(parts[0], out int quantity))
                {
                    throw new ArgumentException($"Invalid quantity : {parts[0]}");
                }

                string robotName = parts[1];

                // Vérification si le robot est valide
                if (!validRobots.Contains(robotName))
                {
                    throw new ArgumentException($"`{robotName}` is not a recongnized robot");
                }

                // Ajout ou mise à jour de la quantité dans le dictionnaire
                if (robotQuantities.ContainsKey(robotName))
                {
                    robotQuantities[robotName] += quantity;
                }
                else
                {
                    robotQuantities[robotName] = quantity;
                }
            }

            return robotQuantities;
        }

        private void ProcessNeededStocks(Dictionary<string, int> robotQuantities)
        {
            this.inventory.GetNeededStocks(robotQuantities);
        }

        private void ProcessInstructions(Dictionary<string, int> robotQuantities)
        {
            Instruction.GenerateAssemblyInstructions(robotQuantities);
        }

        private void ProcessVerify(Dictionary<string, int> robotQuantities)
        {
            if (Order.CheckOrderAvailable(robotQuantities, inventory))
            {
                Console.WriteLine("AVAILABLE");
            }
            else
            {
                Console.WriteLine("UNAVAILABLE");
            }
        }

        private void ProcessProduce(Dictionary<string, int> robotQuantities)
        {
            try {
                if (Order.CheckOrderAvailable(robotQuantities, inventory))
                {
                    this.inventory.RemovePieceInStock(robotQuantities);
                    Console.WriteLine("STOCK_UPDATED");
                }
                else
                {
                    throw new Exception("Not enough pieces in stock");
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"ERROR {ex.Message}");
            }
        }
    }
}