using System;
using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Core.Models;
using src.Robot_factory.Pattern.Factories;
using src.Robot_factory.Pattern.Strategies;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Templates;

public sealed class TemplateManager
{
    private readonly RobotFactoryManager _factoryManager;
    private readonly Dictionary<string, Dictionary<string, int>> _templates;
    private readonly ValidationContext _validationContext;

    public TemplateManager(ValidationContext validationContext, RobotFactoryManager factoryManager)
    {
        _templates = new Dictionary<string, Dictionary<string, int>>();
        _validationContext = validationContext;
        _factoryManager = factoryManager;

        // Ajouter les templates par défaut
        InitializeDefaultTemplates();
    }

    private void InitializeDefaultTemplates()
    {
        // Templates Release 1.0 (compatibilité)
        _templates["RobotI"] = new Dictionary<string, int>
        {
            { "HeartI", 1 },
            { "GeneratorI", 1 },
            { "GripperI", 1 },
            { "WheelI", 1 }
        };

        _templates["RobotII"] = new Dictionary<string, int>
        {
            { "HeartII", 1 },
            { "GeneratorII", 1 },
            { "GripperII", 1 },
            { "WheelII", 1 }
        };

        _templates["RobotIII"] = new Dictionary<string, int>
        {
            { "HeartIII", 1 },
            { "GeneratorIII", 1 },
            { "GripperIII", 1 },
            { "WheelIII", 2 }
        };

        // Templates Release 2.0 - synchronisés avec RobotFactoryManager
        foreach (var robotType in _factoryManager.GetSupportedRobotTypes())
        {
            var robot = _factoryManager.CreateRobot(robotType);
            _templates[robotType] = robot.GetPieces();
        }
    }

    // Template Method Pattern : processus standardisé d'ajout de template
    public string AddTemplate(string templateName, List<string> pieces)
    {
        try
        {
            // Étape 1 : Validation des préconditions
            var preconditionResult = ValidatePreconditions(templateName, pieces);
            if (!preconditionResult.IsValid) return $"ERROR {preconditionResult.ErrorMessage}";

            // Étape 2 : Parser les pièces
            var parsedPieces = ParsePiecesList(pieces);

            // Étape 3 : Déterminer la catégorie du robot
            var category = DetermineRobotCategory(parsedPieces);

            // Étape 4 : Valider les contraintes de construction
            var validationResult = ValidateTemplateConstraints(category, parsedPieces);
            if (!validationResult.IsValid) return $"ERROR {validationResult.ErrorMessage}";

            // Étape 5 : Ajouter le template
            var addResult = AddTemplateToRepository(templateName, parsedPieces, category);
            return !addResult.IsValid ? $"ERROR {addResult.ErrorMessage}" : $"Template {templateName} added successfully";
        }
        catch (Exception ex)
        {
            return $"ERROR {ex.Message}";
        }
    }

    // Étapes du Template Method Pattern

    private ValidationResult ValidatePreconditions(string templateName, List<string> pieces)
    {
        if (string.IsNullOrWhiteSpace(templateName))
            return new ValidationResult(false, "Template name cannot be empty");

        if (_templates.ContainsKey(templateName))
            return new ValidationResult(false, $"Template {templateName} already exists");

        if (pieces == null || pieces.Count == 0)
            return new ValidationResult(false, "Template must contain at least one piece");

        return new ValidationResult(true);
    }

    private static Dictionary<string, int> ParsePiecesList(List<string> pieces)
    {
        var result = new Dictionary<string, int>();

        foreach (var trimmed in pieces.Select(piece => piece.Trim()).Where(trimmed => !string.IsNullOrEmpty(trimmed)))
        {
            if (!PieceCatalog.IsValidPiece(trimmed)) throw new ArgumentException($"Unknown piece: {trimmed}");

            if (result.TryGetValue(trimmed, out var value))
                result[trimmed] = ++value;
            else
                result[trimmed] = 1;
        }

        return result;
    }

    private static RobotCategory DetermineRobotCategory(Dictionary<string, int> pieces)
    {
        var categories = new HashSet<PieceCategory>();

        foreach (var piece in pieces.Keys) categories.Add(PieceCatalog.GetPieceCategory(piece));

        // Logique de détermination de catégorie basée sur les pièces présentes
        if (categories.Contains(PieceCategory.Military))
            return RobotCategory.Military;
        return categories.Contains(PieceCategory.Domestic)
            ? RobotCategory.Domestic
            : RobotCategory.Industrial; // Par défaut pour pièces généralistes
    }

    private ValidationResult ValidateTemplateConstraints(RobotCategory category, Dictionary<string, int> pieces)
    {
        return _validationContext.ValidateConfiguration(category, pieces);
    }

    private ValidationResult AddTemplateToRepository(string templateName, Dictionary<string, int> pieces,
        RobotCategory category)
    {
        try
        {
            _templates[templateName] = pieces;

            // Ajouter au factory manager pour future utilisation
            _factoryManager.AddRobotType(templateName, category);

            return new ValidationResult(true);
        }
        catch (Exception ex)
        {
            return new ValidationResult(false, $"Failed to add template: {ex.Message}");
        }
    }

    // Méthodes publiques pour accès aux templates

    public Dictionary<string, int> GetTemplate(string templateName)
    {
        return _templates.TryGetValue(templateName, out var template) ? new Dictionary<string, int>(template) : null;
    }

    public bool TemplateExists(string templateName)
    {
        return _templates.ContainsKey(templateName);
    }

    public IRobot CreateRobotFromTemplate(string templateName)
    {
        if (!_templates.TryGetValue(templateName, out var value))
            throw new ArgumentException($"Template {templateName} not found");

        // Pour les robots prédéfinis Release 2.0
        if (templateName is "XM-1" or "RD-1" or "WI-1") return _factoryManager.CreateRobot(templateName);

        // Pour les anciens robots Release 1.0
        return templateName switch
        {
            "RobotI" => new RobotI(),
            "RobotII" => new RobotII(),
            "RobotIII" => new RobotIII(),
            _ => new CustomRobot(templateName, value)
        };
    }
}