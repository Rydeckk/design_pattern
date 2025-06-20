using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Pattern.Templates;

namespace src.Robot_factory.Service;

public abstract class Order
{
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
}