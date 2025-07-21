using System;
using System.Collections.Generic;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Pattern.Templates;

namespace src.Robot_factory.Service;

public class Inventory
{
    private readonly Dictionary<string, int> _inventory = new();

    public void AddItem(string itemName, int quantity)
    {
        if (!_inventory.TryAdd(itemName, quantity)) _inventory[itemName] += quantity;
    }

    public int GetItemQuantity(string itemName)
    {
        return _inventory.TryGetValue(itemName, out int value) ? value : 0;
    }

    public void DisplayInventory()
    {
        Console.WriteLine("Current Inventory:");
        foreach (var item in _inventory) Console.WriteLine($"{item.Value} {item.Key}");
    }

    public void GetNeededStocks(Dictionary<string, int> robotQuantities, TemplateManager templateManager = null)
    {
        var robotI = new RobotI();
        var robotII = new RobotII();
        var robotIII = new RobotIII();

        var robotParts = new Dictionary<string, int>();

        foreach (var (robotName, quantity) in robotQuantities)
        {
            Dictionary<string, int> pieces;

            if (templateManager != null && templateManager.TemplateExists(robotName))
            {
                pieces = templateManager.GetTemplate(robotName);
            }
            else
            {
                pieces = robotName switch
                {
                    "RobotI" => robotI.GetPieces(),
                    "RobotII" => robotII.GetPieces(),
                    "RobotIII" => robotIII.GetPieces(),
                    _ => new Dictionary<string, int>()
                };
            }

            Console.WriteLine($"\n{quantity} {robotName} : \n");

            foreach (var (pieceName, value) in pieces)
            {
                var pieceQuantity = value * quantity;

                Console.WriteLine($" {pieceQuantity} {pieceName}");

                if (!robotParts.TryAdd(pieceName, pieceQuantity)) robotParts[pieceName] += pieceQuantity;
            }
        }

        Console.WriteLine("\nTotal : \n");
        foreach (var part in robotParts) Console.WriteLine($" {part.Value} {part.Key}");
    }

    public bool HasItemInQuantity(string itemName, int quantity)
    {
        if (_inventory.TryGetValue(itemName, out var value)) return value >= quantity;
        return false;
    }

    public void RemovePieceInStock(Dictionary<string, int> robotQuantities, TemplateManager templateManager = null)
    {
        foreach (var (robotName, quantity) in robotQuantities)
        {
            Dictionary<string, int> pieces;

            if (templateManager != null && templateManager.TemplateExists(robotName))
                pieces = templateManager.GetTemplate(robotName);
            else
                pieces = robotName switch
                {
                    "RobotI" => new RobotI().GetPieces(),
                    "RobotII" => new RobotII().GetPieces(),
                    "RobotIII" => new RobotIII().GetPieces(),
                    _ => new Dictionary<string, int>()
                };

            foreach (var (pieceName, value) in pieces)
            {
                var pieceQuantity = value * quantity;

                if (!_inventory.ContainsKey(pieceName)) continue;
                _inventory[pieceName] -= pieceQuantity;

                if (_inventory[pieceName] <= 0) _inventory.Remove(pieceName);
            }
        }
    }

    public void RemoveRobotInStock(Dictionary<string, int> robotQuantities)
    {
        foreach (var (robotName, quantity) in robotQuantities)
        {
            if (!_inventory.ContainsKey(robotName)) continue;
            _inventory[robotName] -= Math.Max(0, quantity);
        }
    }
}