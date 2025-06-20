using System;
using System.Collections.Generic;
using src.Robot_factory.Core.Entities;

namespace src.Robot_factory.Service;

public class IndustrialRobot(string robotName = "WI-1") : IRobot
{
    private string Name { get; } = robotName;

    public Dictionary<string, int> GetPieces()
    {
        return new Dictionary<string, int>
        {
            { "Core_CI1", 1 },
            { "Generator_GI1", 1 },
            { "Arms_AI1", 1 },
            { "Legs_LI1", 1 }
        };
    }

    public void AssemblyRule()
    {
        Console.WriteLine("ASSEMBLE CoreAssembly Core_CI1 Generator_GI1");
        Console.WriteLine("INSTALL System_SB1 Core_CI1");
        Console.WriteLine("ASSEMBLE ArmsAssembly CoreAssembly Arms_AI1");
        Console.WriteLine($"ASSEMBLE {Name} ArmsAssembly Legs_LI1");
    }
}