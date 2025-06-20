using System;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Factories;

public class DomesticRobotFactory : IRobotFactory
{
    public IRobot CreateRobot(string robotType)
    {
        return robotType switch
        {
            "RD-1" => new DomesticRobot(),
            _ => throw new ArgumentException($"Robot type {robotType} not supported by Domestic factory")
        };
    }
}