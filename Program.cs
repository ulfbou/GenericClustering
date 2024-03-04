using System;
using System.IO;
using System.Collections.Generic;

using GenericClustering;

internal static class Program
{
    public static void Main(string[] args)
    {
        List<DataPoint<double>> dataPoints = ReadOrGeneratePoints();

        KMeans<double> kMeans = new KMeans<double>(dataPoints, 10);

    }

    public static List<DataPoint<double>> ReadOrGeneratePoints()
    {
        // Ange sökvägen för den lokala filen
        string filePath = "euklidiska_punkter.txt";

        List<DataPoint<double>> dataPoints = new List<DataPoint<double>>();

        // Om filen redan finns, läs in punkterna från filen
        if (File.Exists(filePath))
        {
            Console.WriteLine("Läser in befintliga punkter från filen...");

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Parsa varje rad för att extrahera x- och y-koordinater och skapa en punkt
                    string[] parts = line.Split(';');
                    double x = double.Parse(parts[0]);
                    double y = double.Parse(parts[1]);
                    dataPoints.Add(new DataPoint<double>(x, y));
                }
            }

            Console.WriteLine($"Totalt antal befintliga punkter: {dataPoints.Count}");
        }
        else
        {
            Console.WriteLine("Filen finns inte. Genererar nya slumpmässiga punkter...");

            // Slumpmässigt antal punkter mellan 25 och 150
            Random rand = new Random();
            int numPoints = rand.Next(51, 251);

            // Generera punkterna
            for (int i = 0; i < numPoints; i++)
            {
                double x = rand.NextDouble() * numPoints; // Slumpmässig x-koordinat mellan 0 och 100
                double y = rand.NextDouble() * numPoints; // Slumpmässig y-koordinat mellan 0 och 100
                dataPoints.Add(new DataPoint<double>(x, y));
            }

            Console.WriteLine($"Totalt antal nya slumpmässiga punkter genererade: {numPoints}");

            // Skriv de nya punkterna till filen
            using (StreamWriter writer = File.AppendText(filePath))
            {
                foreach (var dataPoint in dataPoints)
                {
                    writer.WriteLine(dataPoint);
                }
            }

            Console.WriteLine("Nya punkter har sparats i filen.");
        }

        return dataPoints;
    }
}