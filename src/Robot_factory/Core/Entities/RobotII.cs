using System;
using System.Collections.Generic;

namespace src.Robot_factory.Core.Entities;

public class RobotII : IRobot
{
    public Dictionary<string, int> GetPieces()
    {
        var pieces = new Dictionary<string, int>
        {
            { "HeartII", 1 },
            { "GeneratorII", 1 },
            { "GripperII", 1 },
            { "WheelII", 1 }
        };

        return pieces;
    }

    public void AssemblyRule()
    {
        Console.WriteLine("ASSEMBLE A1 HeartII GeneratorII");
        Console.WriteLine("ASSEMBLE A1 GripperII");
        Console.WriteLine("ASSEMBLE A3 [A1,GripperII] WheelII");
    }
}