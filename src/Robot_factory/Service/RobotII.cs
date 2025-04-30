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

        public void GetInstructions()
        {
            throw new NotImplementedException();
        }

        public List<string> GetPieces()
        {
            List<string> pieces = new List<string>();

            pieces.Add("HeartII");
            pieces.Add("GeneratorII");
            pieces.Add("GripperII");
            pieces.Add("WheelII");

            return pieces;
        }
    }
}