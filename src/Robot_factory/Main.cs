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
            inventory.AddItem("RobotI", 2);
            inventory.AddItem("RobotII", 1);
            inventory.AddItem("HeartI", 2);
            inventory.AddItem("HeartII", 1);
            inventory.AddItem("GeneratorI", 2);
            inventory.AddItem("GeneratorII", 1);
            inventory.AddItem("GeneratorIII", 1);
            inventory.AddItem("GripperII", 3);
            inventory.AddItem("GripperIII", 2);
            inventory.AddItem("WheelI", 4);
            inventory.AddItem("WheelII", 1);
            inventory.AddItem("WheelIII", 6);

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