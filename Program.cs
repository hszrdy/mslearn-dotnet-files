using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var storesDirectory = Path.Combine(currentDirectory, "stores");

        var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
        Directory.CreateDirectory(salesTotalDir);

        var salesFiles = FindFiles(storesDirectory);

        var salesTotal = CalculateSalesTotal(salesFiles);

        File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");
        foreach (var file in salesFiles)
        {
            System.Console.WriteLine(file);
        }
    }

    static IEnumerable<string> FindFiles(string folderName)
    {
        List<string> salesFiles = new List<string>();
        var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);


        foreach (var file in foundFiles)
        {
            var extention = Path.GetExtension(file);
            if (extention == ".json")
            {
                salesFiles.Add(file);
            }
        }
        return salesFiles;
    }

    static double CalculateSalesTotal(IEnumerable<string> salesFiles)
    {
        double total = 0;
        foreach (var file in salesFiles)
        {
            var json = File.ReadAllText(file);
            SalesData? data = JsonConvert.DeserializeObject<SalesData?>(json);
            total += data?.Total ?? 0;
        }
        return total;
    }

    record SalesData(double Total);
}