using System;
using System.Collections.Generic;
using System.IO;

public class FileService
{
    public static List<string> ImportInstructionsFromFile(string filePath)
    {
        string format = Path.GetExtension(filePath).ToLower() switch
        {
            ".txt" => "flat",
            ".json" => "json",
            ".xml" => "xml",
            _ => throw new Exception("File format not supported")
        };

        InstructionFileProcessor processor = format switch
        {
            "flat" => new FlatFileProcessor(),
            "json" => new JsonFileProcessor(),
            "xml" => new XmlFileProcessor(),
            _ => throw new Exception("Error on file process")
        };

        return processor.ProcessFile(filePath);
    }

}