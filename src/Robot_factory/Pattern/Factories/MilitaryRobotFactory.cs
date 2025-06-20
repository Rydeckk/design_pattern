using System;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Service;

namespace src.Robot_factory.Pattern.Factories;

public class MilitaryRobotFactory : IRobotFactory
{
    public IRobot CreateRobot(string robotType)
    {
        return robotType switch
        {
            "XM-1" => new MilitaryRobot(),
            _ => throw new ArgumentException($"Robot type {robotType} not supported by Military factory")
        };
    }
}