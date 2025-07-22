using System;
using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Pattern.Templates;

namespace src.Robot_factory.Service;

public class Order
{
    private readonly Dictionary<string, Dictionary<string, int>> _orders = new Dictionary<string, Dictionary<string, int>>();

    public static bool CheckOrderAvailable(Dictionary<string, int> robotQuantities, Inventory inventory,
        TemplateManager templateManager = null)
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

            if ((from piece in pieces
                 let pieceName = piece.Key
                 let pieceQuantity = piece.Value * quantity
                 where !inventory.HasItemInQuantity(pieceName, pieceQuantity)
                 select pieceName).Any())
                return false;
        }

        return true;
    }


    public void MakeOrder(Dictionary<string, int> robotQuantities)
    {
        Guid uuid = Guid.NewGuid();

        string orderId = $"{uuid}";

        _orders[orderId] = robotQuantities;

        Console.WriteLine($"Order id: {orderId}");
    }

    public void SendOrder(string orderId, Dictionary<string, int> robotQuantities, Inventory inventory)
    {
        var updatedRobotQuantities = robotQuantities;

        foreach (var robot in robotQuantities)
        {
            string robotName = robot.Key;
            int quantity = robot.Value;

            if (!_orders[orderId].ContainsKey(robotName)) continue;

            int quantityToRemove = Math.Min(quantity, _orders[orderId][robotName]);

            updatedRobotQuantities[robotName] = quantityToRemove;

            if (_orders[orderId][robotName] >= quantityToRemove)
            {
                _orders[orderId][robotName] -= quantityToRemove;
            }

            if (_orders[orderId][robotName] <= 0)
                _orders[orderId].Remove(robotName);
        }

        inventory.RemoveRobotInStock(updatedRobotQuantities);

        if (_orders[orderId].Count == 0)
        {
            _orders.Remove(orderId);
            Console.WriteLine($"COMPLETED {orderId}");
        }
        else
        {
            Console.WriteLine($"Remaining for {orderId}: {string.Join(", ", _orders[orderId].Select(kv => $"{kv.Key} {kv.Value}"))}");
        }
    }

    public void RemainingOrders()
    {
        foreach (var order in _orders)
        {
            Console.WriteLine($"Remaining for {order.Key}: {string.Join(", ", order.Value.Select(kv => $"{kv.Key} {kv.Value}"))}");
        }
    }

}