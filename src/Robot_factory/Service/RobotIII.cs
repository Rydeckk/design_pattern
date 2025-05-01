using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class RobotIII: Robot
    {
        public string name { get; set; }

        public RobotIII()
        {
            name = "RobotIII";
        }

        public Dictionary<string, int> GetPieces()
        {
            Dictionary<string, int> pieces = new Dictionary<string, int>
            {
                { "HeartIII", 1 },
                { "GeneratorIII", 1 },
                { "GripperIII", 1 },
                { "WheelIII", 1 }
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
}