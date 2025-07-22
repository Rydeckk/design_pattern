using System.Collections.Generic;

public abstract class InstructionFileProcessor
{
    public List<string> ProcessFile(string path)
    {
        string content = ReadFile(path);
        var instructions = ParseInstructions(content);
        return instructions;
    }

    protected abstract string ReadFile(string path);
    protected abstract List<string> ParseInstructions(string content);

}
