using System;
using System.Collections.Generic;
using src.Robot_factory.Core.Entities;

namespace src.Robot_factory.Service;

public class DomesticRobot(string robotName = "RD-1") : IRobot
{
    private string Name { get; } = robotName;

    public Dictionary<string, int> GetPieces()
    {
        return new Dictionary<string, int>
        {
            { "Core_CD1", 1 },
            { "Generator_GD1", 1 },
            { "Arms_AD1", 1 },
            { "Legs_LD1", 1 }
        };
    }

    public void AssemblyRule()
    {
        Console.WriteLine("ASSEMBLE CoreAssembly Core_CD1 Generator_GD1");
        Console.WriteLine("INSTALL System_SB1 Core_CD1");
        Console.WriteLine("ASSEMBLE ArmsAssembly CoreAssembly Arms_AD1");
        Console.WriteLine($"ASSEMBLE {Name} ArmsAssembly Legs_LD1");
    }
}