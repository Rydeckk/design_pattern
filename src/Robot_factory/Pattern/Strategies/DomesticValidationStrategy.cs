using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Core.Models;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Strategies;

public class DomesticValidationStrategy : IValidationStrategy
{
    public ValidationResult ValidateRobotConfiguration(Dictionary<string, int> pieces)
    {
        // Règles pour robots domestiques (D) : peut contenir pièces (D), (G) ou (I)
        foreach (var piece in from piece in pieces.Keys
                 let category = PieceCatalog.GetPieceCategory(piece)
                 where category == PieceCategory.Military
                 select piece)
            return new ValidationResult(false,
                $"Domestic robots cannot use military piece {piece}. Only Domestic (D), Generalist (G) and Industrial (I) pieces are allowed.");

        return new ValidationResult(true);
    }
}