using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class Order
    {
        public static bool CheckOrderAvailable(Dictionary<string, int> robotQuantities, Inventory inventory)
        {
            foreach (var robot in robotQuantities)
            {
                string robotName = robot.Key;
                int quantity = robot.Value;

                Dictionary<string, int> pieces = robotName switch
                    {
                        "RobotI" => new RobotI().GetPieces(),
                        "RobotII" => new RobotII().GetPieces(),
                        "RobotIII" => new RobotIII().GetPieces(),
                        _ => new Dictionary<string, int>()
                    };

                foreach (var piece in pieces)
                {
                    string pieceName = piece.Key;
                    int pieceQuantity = piece.Value * quantity;

                    if (!inventory.HasItem(pieceName, pieceQuantity))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
    }
}