using System;
using System.Collections.Generic;
using src.Robot_factory.Core.Entities;

namespace src.Robot_factory.Service;

public class MilitaryRobot(string robotName = "XM-1") : IRobot
{
    private string Name { get; } = robotName;

    public Dictionary<string, int> GetPieces()
    {
        return new Dictionary<string, int>
        {
            { "Core_CM1", 1 },
            { "Generator_GM1", 1 },
            { "Arms_AM1", 1 },
            { "Legs_LM1", 1 }
        };
    }

    public void AssemblyRule()
    {
        Console.WriteLine("ASSEMBLE CoreAssembly Core_CM1 Generator_GM1");
        Console.WriteLine("INSTALL System_SB1 Core_CM1");
        Console.WriteLine("ASSEMBLE ArmsAssembly CoreAssembly Arms_AM1");
        Console.WriteLine($"ASSEMBLE {Name} ArmsAssembly Legs_LM1");
    }
}