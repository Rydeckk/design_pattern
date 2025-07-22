using System;
using System.Collections.Generic;
using System.IO;

public class FileService
{
    public static List<string> ImportInstructionsFromFile(string filePath)
    {
        string format = GetFormatFromExtension(filePath);

        InstructionFileProcessor processor = GetProcessorByFormat(format);

        return processor.ProcessFile(filePath);
    }

    public static void ExportOutputToFile(string filePath, List<string> outputLines)
    {
        string format = GetFormatFromExtension(filePath);

        InstructionFileProcessor processor = GetProcessorByFormat(format);

        processor.SaveOutputToFile(filePath, outputLines);
    }

    public static string GetFormatFromExtension(string filePath)
    {
        return Path.GetExtension(filePath).ToLower() switch
        {
            ".txt" => "flat",
            ".json" => "json",
            ".xml" => "xml",
            _ => throw new Exception("File format not supported")
        };
    }
    public static InstructionFileProcessor GetProcessorByFormat(string format)
    {
        return format switch
        {
            "flat" => new FlatFileProcessor(),
            "json" => new JsonFileProcessor(),
            "xml" => new XmlFileProcessor(),
            _ => throw new Exception("Error on file process")
        };
    }

}