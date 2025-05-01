using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class RobotI : Robot
    {
        public string name { get; set; }

        public RobotI()
        {
            name = "RobotI";
        }

        public Dictionary<string, int> GetPieces()
        {
            Dictionary<string, int> pieces = new Dictionary<string, int>
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
}