using System.Collections.Generic;

namespace src.Robot_factory.Core.Entities;

public interface IRobot
{
    Dictionary<string, int> GetPieces();

    void AssemblyRule();
}