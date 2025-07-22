using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using src.Robot_factory.Pattern.Templates;
using src.Robot_factory.Service;

namespace src.Robot_factory.Controller;

public class Command(Inventory inventory, Order order, TemplateManager templateManager)
{
    private readonly List<string> _validRobots = ["RobotI", "RobotII", "RobotIII", "XM-1", "RD-1", "WI-1"];

    protected List<string> _outputLines = [];

    public void Execute(string command, string[] args, string input)
    {
        var robotQuantities = new Dictionary<string, int>();

        string[] validCommands = ["STOCKS", "ADD_TEMPLATE", "LIST_ORDER", "RECEIVE", "IMPORT", "EXPORT"];

        bool isInvalidCommand = !validCommands.Any(c => c.Equals(command, StringComparison.CurrentCultureIgnoreCase));

        if (isInvalidCommand)
            try
            {
                robotQuantities = command.Equals("SEND", StringComparison.CurrentCultureIgnoreCase) ?
                ProcessArgs([.. args.Skip(1)]) :
                ProcessArgs(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR {ex.Message}");
                return;
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
            case "ADD_TEMPLATE":
                ProcessAddTemplate(args);
                break;
            case "ORDER":
                ProcessOrder(robotQuantities);
                break;
            case "SEND":
                var orderId = args[0].Trim();
                ProcessSendOrder(orderId, robotQuantities);
                break;
            case "LIST_ORDER":
                ProcessRemainingOrders();
                break;
            case "RECEIVE":
                var inventoryQuantities = ProcessArgsInventory(args);
                ProcessReceive(inventoryQuantities);
                break;
            case "IMPORT":
                var importPath = args[0].Trim();
                ProcessImport(importPath);
                break;
            case "EXPORT":
                var exportPath = args[0].Trim();
                ProcessOutput(exportPath);
                break;
            default:
                Console.WriteLine("Unknown command.");
                break;
        }

        if (!command.Equals("EXPORT", StringComparison.CurrentCultureIgnoreCase))
        {
            _outputLines.Add(input.ToUpper());
        }

    }

    private void ProcessStocks()
    {
        inventory.DisplayInventory();
    }

    private Dictionary<string, int> ProcessArgs(string[] args)
    {
        var robotQuantities = new Dictionary<string, int>();

        foreach (var arg in args)
        {
            // Chaque argument est sous la forme "2 RobotI"
            var parts = arg.Trim().Split(' ');
            if (parts.Length != 2) throw new ArgumentException($"Invalid argument format : {arg}");

            if (!int.TryParse(parts[0], out var quantity))
                throw new ArgumentException($"Invalid quantity : {parts[0]}");

            var robotName = parts[1];

            // Vérification si le robot est valide (inclut templates personnalisés)
            if (!_validRobots.Contains(robotName) && !templateManager.TemplateExists(robotName))
                throw new ArgumentException($"`{robotName}` is not a recognized robot");

            // Ajout ou mise à jour de la quantité dans le dictionnaire
            if (!robotQuantities.TryAdd(robotName, quantity)) robotQuantities[robotName] += quantity;
        }

        return robotQuantities;
    }

    private Dictionary<string, int> ProcessArgsInventory(string[] args)
    {
        var inventoryQuantities = new Dictionary<string, int>();

        foreach (var arg in args)
        {
            var parts = arg.Trim().Split(' ');
            if (parts.Length != 2) throw new ArgumentException($"Invalid argument format : {arg}");

            if (!int.TryParse(parts[0], out var quantity))
                throw new ArgumentException($"Invalid quantity : {parts[0]}");

            var itemtName = parts[1];

            if (!inventory.Contains(itemtName) && !templateManager.TemplateExists(itemtName))
                throw new ArgumentException($"`{itemtName}` is not a recognized inventory item");

            if (!inventoryQuantities.TryAdd(itemtName, quantity)) inventoryQuantities[itemtName] += quantity;
        }

        return inventoryQuantities;
    }

    private void ProcessNeededStocks(Dictionary<string, int> robotQuantities)
    {
        inventory.GetNeededStocks(robotQuantities, templateManager);
    }

    private void ProcessInstructions(Dictionary<string, int> robotQuantities)
    {
        Instruction.GenerateAssemblyInstructions(robotQuantities, templateManager);
    }

    private void ProcessAddTemplate(string[] args)
    {
        try
        {
            var joinedArgs = string.Join(" ", args);

            if (string.IsNullOrEmpty(joinedArgs) || !joinedArgs.Contains(','))
            {
                Console.WriteLine(
                    "ERROR Invalid ADD_TEMPLATE format. Expected: ADD_TEMPLATE TEMPLATE_NAME, Piece1, Piece2, ...");
                return;
            }

            var parts = joinedArgs.Split(',');
            var templateName = parts[0].Trim();
            var pieces = parts.Skip(1).Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();

            if (pieces.Count == 0)
            {
                Console.WriteLine("ERROR Template must contain at least one piece");
                return;
            }

            var result = templateManager.AddTemplate(templateName, pieces);
            Console.WriteLine(result);

            // Mettre à jour la liste des robots valides si le template a été ajouté avec succès
            if (result.StartsWith("Template") && result.Contains("added successfully")) _validRobots.Add(templateName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR {ex.Message}");
        }
    }

    private void ProcessVerify(Dictionary<string, int> robotQuantities)
    {
        Console.WriteLine(Order.CheckOrderAvailable(robotQuantities, inventory, templateManager)
            ? "AVAILABLE"
            : "UNAVAILABLE");
    }

    private void ProcessProduce(Dictionary<string, int> robotQuantities)
    {
        try
        {
            if (Order.CheckOrderAvailable(robotQuantities, inventory, templateManager))
            {
                inventory.RemovePieceInStock(robotQuantities, templateManager);
                Console.WriteLine("STOCK_UPDATED");
            }
            else
            {
                throw new Exception("Not enough pieces in stock");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR {ex.Message}");
        }
    }

    private void ProcessOrder(Dictionary<string, int> robotQuantities)
    {
        try
        {
            if (Order.CheckOrderAvailable(robotQuantities, inventory, templateManager))
            {
                order.MakeOrder(robotQuantities);
            }
            else
            {
                throw new Exception("Not enough robots in stock");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR {ex.Message}");
        }
    }

    private void ProcessSendOrder(string orderId, Dictionary<string, int> robotQuantities)
    {
        order.SendOrder(orderId, robotQuantities, inventory);
    }

    private void ProcessRemainingOrders()
    {
        order.RemainingOrders();
    }

    private void ProcessReceive(Dictionary<string, int> inventoryQuantities)
    {
        foreach (var (itemName, quantity) in inventoryQuantities)
        {
            inventory.AddItem(itemName, quantity);
        }
    }

    private void ProcessImport(string importPath)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Robot_factory", "Files", importPath);
        if (!File.Exists(path))
        {
            Console.WriteLine($"ERROR: file '{path}' was not found");
        }
        else
        {
            Console.WriteLine($"IMPORTED: {importPath}");

            var instructions = FileService.ImportInstructionsFromFile(path);

            foreach (var instruction in instructions)
            {
                if (string.IsNullOrEmpty(instruction) || instruction.Equals("EXIT", StringComparison.CurrentCultureIgnoreCase)) break;

                var parts = instruction.Split(' ');
                var commandName = parts[0].ToUpper();
                var argsString = string.Join(' ', parts.Skip(1));
                var commandArgs = CommandService.ProcessCommandArgs(commandName, argsString);

                Execute(commandName, commandArgs, instruction);
            }
        }
    }

    private void ProcessOutput(string exportPath)
    {
        FileService.ExportOutputToFile(exportPath, _outputLines);
        Console.WriteLine("EXPORTED");
        _outputLines = [];
    }
}