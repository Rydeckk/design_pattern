using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class JsonFileProcessor : InstructionFileProcessor
{
    protected override string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }

    protected override List<string> ParseInstructions(string content)
    {
        return JsonSerializer.Deserialize<List<string>>(content) ?? [];
    }

    protected override string FormatOutput(List<string> lines)
    {
        return JsonSerializer.Serialize(lines, new JsonSerializerOptions { WriteIndented = true });
    }
}
