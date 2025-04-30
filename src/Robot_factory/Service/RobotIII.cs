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

        public void GetInstructions()
        {
            throw new NotImplementedException();
        }

        public List<string> GetPieces()
        {
            List<string> pieces = new List<string>();

            pieces.Add("HeartIII");
            pieces.Add("GeneratorIII");
            pieces.Add("GripperIII");
            pieces.Add("WheelIII");

            return pieces;
        }
    }
}