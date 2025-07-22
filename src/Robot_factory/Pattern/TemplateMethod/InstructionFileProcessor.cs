using System;
using System.Collections.Generic;
using System.IO;

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

    public void SaveOutputToFile(string exportPath, List<string> outputLines)
    {
        Guid uuid = Guid.NewGuid();
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Robot_factory", "Files", uuid + exportPath);
        string formattedContent = FormatOutput(outputLines);
        WriteFile(path, formattedContent);
    }

    protected abstract string FormatOutput(List<string> lines);

    protected virtual void WriteFile(string path, string content)
    {
        File.WriteAllText(path, content);
    }
}
