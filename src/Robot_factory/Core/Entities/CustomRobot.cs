using System;
using System.Collections.Generic;
using System.Linq;

namespace src.Robot_factory.Core.Entities;

public class CustomRobot(string robotName, Dictionary<string, int> pieces) : IRobot
{
    private readonly Dictionary<string, int> _pieces = new(pieces);
    public string Name { get; set; } = robotName;

    public Dictionary<string, int> GetPieces()
    {
        return new Dictionary<string, int>(_pieces);
    }

    public void AssemblyRule()
    {
        // Règles d'assemblage génériques pour robots personnalisés
        var pieceList = _pieces.Keys.ToList();

        switch (pieceList.Count)
        {
            case >= 2:
            {
                Console.WriteLine($"ASSEMBLE CoreAssembly {pieceList[0]} {pieceList[1]}");

                if (pieceList.Count >= 3) Console.WriteLine($"ASSEMBLE MainAssembly CoreAssembly {pieceList[2]}");

                switch (pieceList.Count)
                {
                    case >= 4:
                        Console.WriteLine($"ASSEMBLE {Name} MainAssembly {pieceList[3]}");
                        break;
                    case 3:
                        Console.WriteLine($"ASSEMBLE {Name} MainAssembly");
                        break;
                    default:
                        Console.WriteLine($"ASSEMBLE {Name} CoreAssembly");
                        break;
                }

                break;
            }
            case 1:
                Console.WriteLine($"ASSEMBLE {Name} {pieceList[0]}");
                break;
        }
    }
}