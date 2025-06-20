using System;
using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Core.Models;

namespace src.Robot_factory.Pattern.Factories;

public class RobotFactoryManager
{
    private readonly Dictionary<RobotCategory, IRobotFactory> _factories = new()
    {
        { RobotCategory.Military, new MilitaryRobotFactory() },
        { RobotCategory.Domestic, new DomesticRobotFactory() },
        { RobotCategory.Industrial, new IndustrialRobotFactory() }
    };

    private readonly Dictionary<string, RobotCategory> _robotCategoryMap = new()
    {
        // Release 2.0 robots par catégorie
        { "XM-1", RobotCategory.Military },
        { "RD-1", RobotCategory.Domestic },
        { "WI-1", RobotCategory.Industrial }
    };

    // Release 2.0 robots par catégorie

    public IRobot CreateRobot(string robotType)
    {
        if (!_robotCategoryMap.TryGetValue(robotType, out var category))
            throw new ArgumentException($"Unknown robot type: {robotType}");

        var factory = _factories[category];
        return factory.CreateRobot(robotType);
    }

    public List<string> GetSupportedRobotTypes()
    {
        return _robotCategoryMap.Keys.ToList();
    }

    public void AddRobotType(string robotType, RobotCategory category)
    {
        _robotCategoryMap[robotType] = category;
    }
}