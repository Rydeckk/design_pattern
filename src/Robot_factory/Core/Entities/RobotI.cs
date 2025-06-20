using System;
using System.Collections.Generic;

namespace src.Robot_factory.Core.Entities;

public class RobotI : IRobot
{
    public string Name { get; set; } = "RobotI";

    public Dictionary<string, int> GetPieces()
    {
        var pieces = new Dictionary<string, int>
        {
            { "HeartI", 1 },
            { "GeneratorI", 1 },
            { "GripperI", 1 },
            { "WheelI", 1 }
        };

        return pieces;
    }

    public void AssemblyRule()
    {
        Console.WriteLine("ASSEMBLE A1 HeartI GeneratorI");
        Console.WriteLine("ASSEMBLE A1 GripperI");
        Console.WriteLine("ASSEMBLE A3 [A1,GripperI] WheelI");
    }
}