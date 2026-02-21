using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var storesDirectory = Path.Combine(currentDirectory, "stores");

        var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
        Directory.CreateDirectory(salesTotalDir);

        var salesFiles = FindFiles(storesDirectory);

        File.WriteAllText(Path.Combine(salesTotalDir, "totals.txt"), string.Empty);

        foreach (var file in salesFiles)
        {
            System.Console.WriteLine(file);
        }
    }

    static IEnumerable<string> FindFiles(string folderName)
    {
        List<string> salesFiles = new List<string>();
        var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

        /*
        foreach (var file in foundFiles)
        {
            var extention = Path.GetExtension(file);
            if (extention == ".json")
            {
                salesFiles.Add(file);
            }
        }*/
        return salesFiles;
    }
}