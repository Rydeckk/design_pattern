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

        public void GetInstructions()
        {
            throw new NotImplementedException();
        }

        public List<string> GetPieces()
        {
            List<string> pieces = new List<string>();

            pieces.Add("HeartI");
            pieces.Add("GeneratorI");
            pieces.Add("GripperI");
            pieces.Add("WheelI");

            return pieces;
        }

        public static implicit operator RobotI(RobotIII v)
        {
            throw new NotImplementedException();
        }
    }
}