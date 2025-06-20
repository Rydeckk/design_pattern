using System;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Factories;

public class IndustrialRobotFactory : IRobotFactory
{
    public IRobot CreateRobot(string robotType)
    {
        return robotType switch
        {
            "WI-1" => new IndustrialRobot(),
            _ => throw new ArgumentException($"Robot type {robotType} not supported by Industrial factory")
        };
    }
}