using System.Collections.Generic;

namespace src.Robot_factory.Pattern.Strategies;

public interface IValidationStrategy
{
    ValidationResult ValidateRobotConfiguration(Dictionary<string, int> pieces);
}

public class ValidationResult(bool isValid, string errorMessage = "")
{
    public bool IsValid { get; } = isValid;
    public string ErrorMessage { get; } = errorMessage;
}