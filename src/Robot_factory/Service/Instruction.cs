using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Robot_factory.Service
{
    public class Instruction
    {
        public static void GenerateAssemblyInstructions(Dictionary<string, int> robotQuantities)
        {
            var robotI = new RobotI();
            var robotII = new RobotII();
            var robotIII = new RobotIII();

            foreach (var robot in robotQuantities)
            {
                string robotName = robot.Key;
                int quantity = robot.Value;

                Dictionary<string, int> pieces = robotName switch
                    {
                        "RobotI" => robotI.GetPieces(),
                        "RobotII" => robotII.GetPieces(),
                        "RobotIII" => robotIII.GetPieces(),
                        _ => new Dictionary<string, int>()
                    };

                for (int i = 0; i < quantity; i++)
                {

                    Console.WriteLine($"\nPRODUCING {robotName}");
                    
                    foreach (var piece in pieces)
                    {
                        Console.WriteLine($"GET_OUT_STOCK {piece.Value} {piece.Key}");
                    }

                    CheckInstallationRequired(pieces.Keys.ToList());

                    switch (robotName)
                    {
                        case "RobotI":
                            robotI.AssemblyRule();
                            break;
                        case "RobotII":
                            robotII.AssemblyRule();
                            break;
                        case "RobotIII":
                            robotIII.AssemblyRule();
                            break;
                        default:
                            throw new NotImplementedException();
                    }

                    Console.WriteLine($"FINISHED {robotName}");
                }
            }
        }

        private static void CheckInstallationRequired(List<string> pieces)
        {
            var installationRequirements = new Dictionary<string, string>
            {
                { "HeartI", "SystemXMI" },
                { "HeartII", "SystemXMI" },
                { "HeartIII", "SystemXMI" }
            };

            foreach (var piece in pieces)
            {
                if (installationRequirements.ContainsKey(piece))
                {
                    string system = installationRequirements[piece];
                    Console.WriteLine($"INSTALL {system} {piece}");
                }
            }
        }
    }
}