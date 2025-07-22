using System;
using System.Linq;
using src.Robot_factory.Controller;
using src.Robot_factory.Pattern.Factories;
using src.Robot_factory.Pattern.Strategies;
using src.Robot_factory.Pattern.Templates;
using src.Robot_factory.Service;

namespace src.Robot_factory;

public abstract class RobotFactory
{
    public static void Main()
    {
        // Initialisation du stock
        var inventory = new Inventory();
        inventory.AddItem("RobotI", 2);
        inventory.AddItem("RobotII", 1);
        inventory.AddItem("HeartI", 2);
        inventory.AddItem("HeartII", 1);
        inventory.AddItem("HeartIII", 2);
        inventory.AddItem("GeneratorI", 2);
        inventory.AddItem("GeneratorII", 1);
        inventory.AddItem("GeneratorIII", 1);
        inventory.AddItem("GripperI", 2);
        inventory.AddItem("GripperII", 3);
        inventory.AddItem("GripperIII", 2);
        inventory.AddItem("WheelI", 4);
        inventory.AddItem("WheelII", 1);
        inventory.AddItem("WheelIII", 6);

        // Initialisation des nouveaux services Release 2.0
        var factoryManager = new RobotFactoryManager();
        var validationContext = new ValidationContext();
        var templateManager = new TemplateManager(validationContext, factoryManager);

        // Ajouter les nouvelles pièces au stock
        inventory.AddItem("Core_CM1", 3);
        inventory.AddItem("Core_CD1", 2);
        inventory.AddItem("Core_CI1", 4);
        inventory.AddItem("Generator_GM1", 3);
        inventory.AddItem("Generator_GD1", 2);
        inventory.AddItem("Generator_GI1", 4);
        inventory.AddItem("Arms_AM1", 3);
        inventory.AddItem("Arms_AD1", 2);
        inventory.AddItem("Arms_AI1", 4);
        inventory.AddItem("Legs_LM1", 3);
        inventory.AddItem("Legs_LD1", 2);
        inventory.AddItem("Legs_LI1", 4);
        inventory.AddItem("System_SB1", 10);
        inventory.AddItem("System_SM1", 3);
        inventory.AddItem("System_SD1", 2);
        inventory.AddItem("System_SI1", 4);

        var order = new Order();

        // Initialisation du contrôleur de commandes
        var command = new Command(inventory, order, templateManager);

        while (true)
        {
            Console.WriteLine("Enter command (EXIT to quit) : ");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input.Equals("EXIT", StringComparison.CurrentCultureIgnoreCase)) break;

            var parts = input.Split(' ');
            var commandName = parts[0].ToUpper();
            var argsString = string.Join(' ', parts.Skip(1));
            var commandArgs = CommandService.ProcessCommandArgs(commandName, argsString);

            command.Execute(commandName, commandArgs, input);
        }
    }
}