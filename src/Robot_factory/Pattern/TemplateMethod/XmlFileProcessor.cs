using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public class XmlFileProcessor : InstructionFileProcessor
{
    protected override string ReadFile(string path)
    {
        return File.ReadAllText(path);
    }

    protected override List<string> ParseInstructions(string content)
    {
        var doc = XDocument.Parse(content);
        return [.. doc.Descendants("Instruction").Select(e => e.Value.Trim())];
    }

}
