using System.Collections.Generic;
using src.Robot_factory.Core.Models;

namespace src.Robot_factory.Service;

public abstract class PieceCatalog
{
    private static readonly Dictionary<string, PieceCategory> PieceCategories = new()
    {
        // Pièces Release 1.0 - considérées comme Généralistes pour compatibilité
        { "HeartI", PieceCategory.Generalist },
        { "HeartII", PieceCategory.Generalist },
        { "HeartIII", PieceCategory.Generalist },
        { "GeneratorI", PieceCategory.Generalist },
        { "GeneratorII", PieceCategory.Generalist },
        { "GeneratorIII", PieceCategory.Generalist },
        { "GripperI", PieceCategory.Generalist },
        { "GripperII", PieceCategory.Generalist },
        { "GripperIII", PieceCategory.Generalist },
        { "WheelI", PieceCategory.Generalist },
        { "WheelII", PieceCategory.Generalist },
        { "WheelIII", PieceCategory.Generalist },

        // Nouveaux modules principaux (Release 2.0)
        { "Core_CM1", PieceCategory.Military },
        { "Core_CD1", PieceCategory.Domestic },
        { "Core_CI1", PieceCategory.Industrial },

        // Nouveaux générateurs
        { "Generator_GM1", PieceCategory.Military },
        { "Generator_GD1", PieceCategory.Domestic },
        { "Generator_GI1", PieceCategory.Industrial },

        // Nouveaux modules de préhension
        { "Arms_AM1", PieceCategory.Military },
        { "Arms_AD1", PieceCategory.Domestic },
        { "Arms_AI1", PieceCategory.Industrial },

        // Nouveaux modules de déplacement
        { "Legs_LM1", PieceCategory.Military },
        { "Legs_LD1", PieceCategory.Domestic },
        { "Legs_LI1", PieceCategory.Industrial }
    };

    public static PieceCategory GetPieceCategory(string pieceName)
    {
        return PieceCategories.GetValueOrDefault(pieceName, PieceCategory.Generalist);
    }

    public static bool IsValidPiece(string pieceName)
    {
        return PieceCategories.ContainsKey(pieceName);
    }
}