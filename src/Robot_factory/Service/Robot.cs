using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public interface Robot
    {
        string name { get; set; }
        List<string> GetPieces();
        void GetInstructions();
    }
}