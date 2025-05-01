using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Robot_factory.Service;
using src.Robot_factory.Controller;

namespace src.Robot_factory
{
    public class RobotFactory
    {
        public static void Main(string[] args)
        {
            // Initialisation du stock
            Inventory inventory = new Inventory();
            inventory.AddItem("Robot1", 10);
            inventory.AddItem("Robot2", 5);
            inventory.AddItem("Piece1", 20);
            inventory.AddItem("Piece2", 15);

            // Initialisation du contrôleur de commandes
            Command command = new Command(inventory);

            while (true)
            {
                Console.WriteLine("Enter command (EXIT to quit) : ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "EXIT")
                {
                    break;
                }

                string[] parts = input.Split(' ');
                string commandName = parts[0].ToUpper();
                string argsString = string.Join(' ', parts.Skip(1));
                string[] commandArgs = argsString.Split(',').ToArray();

                command.Execute(commandName, commandArgs);
            }
        }
    }
}