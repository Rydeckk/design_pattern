using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class FlatFileProcessor : InstructionFileProcessor
{
    protected override string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }

    protected override List<string> ParseInstructions(string content)
    {
        return [.. content.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim())];
    }

    protected override string FormatOutput(List<string> lines)
    {
        return string.Join(Environment.NewLine, lines);
    }
}
