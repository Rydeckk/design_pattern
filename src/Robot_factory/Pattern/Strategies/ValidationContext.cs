using System.Collections.Generic;
using src.Robot_factory.Core.Models;

namespace src.Robot_factory.Pattern.Strategies;

public class ValidationContext
{
    private readonly Dictionary<RobotCategory, IValidationStrategy> _strategies = new()
    {
        { RobotCategory.Military, new MilitaryValidationStrategy() },
        { RobotCategory.Domestic, new DomesticValidationStrategy() },
        { RobotCategory.Industrial, new IndustrialValidationStrategy() }
    };

    public ValidationResult ValidateConfiguration(RobotCategory category, Dictionary<string, int> pieces)
    {
        return !_strategies.TryGetValue(category, out var strategy) ? new ValidationResult(false, $"Unknown robot category: {category}") : strategy.ValidateRobotConfiguration(pieces);
    }
}