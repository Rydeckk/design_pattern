using System.Collections.Generic;
using src.Robot_factory.Core.Models;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Strategies;

public class IndustrialValidationStrategy : IValidationStrategy
{
    public ValidationResult ValidateRobotConfiguration(Dictionary<string, int> pieces)
    {
        // Règles pour robots industriels (I) : peut contenir pièces (G) ou (I)
        foreach (var piece in pieces.Keys)
        {
            var category = PieceCatalog.GetPieceCategory(piece);
            if (category != PieceCategory.Generalist && category != PieceCategory.Industrial)
                return new ValidationResult(false,
                    $"Industrial robots cannot use piece {piece} of category {category}. Only Generalist (G) and Industrial (I) pieces are allowed.");
        }

        return new ValidationResult(true);
    }
}