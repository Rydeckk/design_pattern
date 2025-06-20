using src.Robot_factory.Core.Entities;

namespace src.Robot_factory.Pattern.Factories;

public interface IRobotFactory
{
    IRobot CreateRobot(string robotType);
}