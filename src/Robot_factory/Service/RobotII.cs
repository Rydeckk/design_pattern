using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class RobotII: Robot
    {
        public string name { get; set; }

        public RobotII()
        {
            name = "RobotII";
        }

        public Dictionary<string, int> GetPieces()
        {
            Dictionary<string, int> pieces = new Dictionary<string, int>
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
}