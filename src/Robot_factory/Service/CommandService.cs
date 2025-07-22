public class CommandService
{
    public static string[] ProcessCommandArgs(string commandName, string argsString)
    {
        if (commandName == "ADD_TEMPLATE") return [argsString];

        return argsString.Split(',');
    }
}