using System;
using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Pattern.Templates;
using src.Robot_factory.Service;

namespace src.Robot_factory.Controller;

public class Command(Inventory inventory, TemplateManager templateManager)
{
    private readonly List<string> _validRobots = ["RobotI", "RobotII", "RobotIII", "XM-1", "RD-1", "WI-1"];

    public void Execute(string command, string[] args)
    {
        var robotQuantities = new Dictionary<string, int>();

        if (!command.Equals("STOCKS", StringComparison.CurrentCultureIgnoreCase) &&
            !command.Equals("ADD_TEMPLATE", StringComparison.CurrentCultureIgnoreCase))
            try
            {
                robotQuantities = ProcessArgs(args);
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
            default:
                Console.WriteLine("Unknown command.");
                break;
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
}