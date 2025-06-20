using System.Collections.Generic;
using src.Robot_factory.Core.Models;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Strategies;

public class MilitaryValidationStrategy : IValidationStrategy
{
    public ValidationResult ValidateRobotConfiguration(Dictionary<string, int> pieces)
    {
        // Règles pour robots militaires (M) : peut contenir pièces (M) ou (I) et systèmes (M) ou (G)
        foreach (var piece in pieces.Keys)
        {
            var category = PieceCatalog.GetPieceCategory(piece);
            if (category != PieceCategory.Military && category != PieceCategory.Industrial)
                return new ValidationResult(false,
                    $"Military robots cannot use piece {piece} of category {category}. Only Military (M) and Industrial (I) pieces are allowed.");
        }

        return new ValidationResult(true);
    }
}