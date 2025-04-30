using System;
using System.Collections.Generic;
using System.Linq;
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
            switch (command.ToUpper())
            {
                case "STOCKS":
                    inventory.DisplayInventory();
                    break;
                case "NEEDED_STOCKS":
                    var robotQuantities = ProcessArgs(args);
                    inventory.GetNeededStocks(robotQuantities);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
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
                    Console.WriteLine($"Invalid argument: {arg}");
                    continue;
                }

                if (!int.TryParse(parts[0], out int quantity))
                {
                    Console.WriteLine($"Invalid quantity: {parts[0]}");
                    continue;
                }

                string robotName = parts[1];

                // Vérification si le robot est valide
                if (!validRobots.Contains(robotName))
                {
                    Console.WriteLine($"Invalid robot: {robotName}");
                    continue;
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
    }
}