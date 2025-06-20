using System;
using System.Collections.Generic;

namespace src.Robot_factory.Core.Entities;

public class RobotIII : IRobot
{
    public Dictionary<string, int> GetPieces()
    {
        var pieces = new Dictionary<string, int>
        {
            { "HeartIII", 1 },
            { "GeneratorIII", 1 },
            { "GripperIII", 1 },
            { "WheelIII", 2 }
        };

        return pieces;
    }

    public void AssemblyRule()
    {
        Console.WriteLine("ASSEMBLE A1 HeartIII GeneratorIII");
        Console.WriteLine("ASSEMBLE A1 GripperIII");
        Console.WriteLine("ASSEMBLE A3 [A1,GripperIII] WheelIII");
    }
}