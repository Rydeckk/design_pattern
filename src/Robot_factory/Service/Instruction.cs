using System;
using System.Collections.Generic;
using System.Linq;
using src.Robot_factory.Core.Entities;
using src.Robot_factory.Pattern.Templates;

namespace src.Robot_factory.Service;

public abstract class Instruction
{
    public static void GenerateAssemblyInstructions(Dictionary<string, int> robotQuantities,
        TemplateManager templateManager = null)
    {
        var robotI = new RobotI();
        var robotII = new RobotII();
        var robotIII = new RobotIII();

        foreach (var (robotName, quantity) in robotQuantities)
        {
            Dictionary<string, int> pieces;
            IRobot robotInstance;

            // Essayer d'abord les templates personnalisés
            if (templateManager != null && templateManager.TemplateExists(robotName))
            {
                pieces = templateManager.GetTemplate(robotName);
                robotInstance = templateManager.CreateRobotFromTemplate(robotName);
            }
            else
            {
                // Fallback sur les robots existants
                pieces = robotName switch
                {
                    "RobotI" => robotI.GetPieces(),
                    "RobotII" => robotII.GetPieces(),
                    "RobotIII" => robotIII.GetPieces(),
                    _ => new Dictionary<string, int>()
                };

                robotInstance = robotName switch
                {
                    "RobotI" => robotI,
                    "RobotII" => robotII,
                    "RobotIII" => robotIII,
                    _ => null
                };
            }

            for (var i = 0; i < quantity; i++)
            {
                Console.WriteLine($"\nPRODUCING {robotName}");

                foreach (var piece in pieces) Console.WriteLine($"GET_OUT_STOCK {piece.Value} {piece.Key}");

                CheckInstallationRequired(pieces.Keys.ToList());

                // Utiliser l'instance du robot pour les règles d'assemblage
                if (robotInstance != null)
                    robotInstance.AssemblyRule();
                else
                    Console.WriteLine($"WARNING: No assembly rules defined for {robotName}");

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
            if (!installationRequirements.TryGetValue(piece, out var system)) continue;
            Console.WriteLine($"INSTALL {system} {piece}");
        }
    }
}