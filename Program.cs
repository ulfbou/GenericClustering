using GenericClustering;
using GenericClustering.ClusterAlgorithms;
using GenericClustering.CoefficientCalculators;

public static class Program
{
    static void Main(string[] args)
    {
        List<Cluster<double>> clusters = PopulateClusters().ToList();
        //List<Cluster<double>> clusters = Program.ReadOrGeneratePoints().ToList();
        List<IDataPoint<double>> dataPoints = clusters.SelectMany(cluster => cluster.GetAllDataPoints()).ToList();

        var silhouetteCalculator = new SilhouetteCalculator<double>(clusters);
        double silhouetteScore = silhouetteCalculator.Evaluate();
        Console.WriteLine($"Silhouette Score: {silhouetteScore}");

        // Run the data points against KMeans to find a new clustering. 
        KMeans<double> kMeans = new KMeans<double>(dataPoints, 4);
        kMeans.Run(100);

        clusters = kMeans.GetAllClusters().ToList();
        silhouetteCalculator = new SilhouetteCalculator<double>(clusters);
        silhouetteScore = silhouetteCalculator.Evaluate();
        Console.WriteLine($"Silhouette Score: {silhouetteScore}");
    }


    internal static IEnumerable<Cluster<double>> PopulateClusters()
    {
        List<Cluster<double>> clusters = new List<Cluster<double>>
        {
            new Cluster<double> (new List<IDataPoint<double>> {
                new DataPoint<double>(8.35, 160.46),
                new DataPoint<double>(9.83, 178.88),
                new DataPoint<double>(31.11, 150.34),
                new DataPoint<double>(31.28, 147.32),
                new DataPoint<double>(44.75, 148.82),
                new DataPoint<double>(52.11, 147.3),
                new DataPoint<double>(53.81, 154.02),
                new DataPoint<double>(53.91, 155.75),
                new DataPoint<double>(55.22, 131.33),
                new DataPoint<double>(57.31, 165.51),
                new DataPoint<double>(58.85, 165.46),
                new DataPoint<double>(59.82, 130.52),
                new DataPoint<double>(60.96, 139.71),
                new DataPoint<double>(61.59, 177.51),
                new DataPoint<double>(61.76, 133.04),
                new DataPoint<double>(61.78, 160.88),
                new DataPoint<double>(64.33, 147.87),
                new DataPoint<double>(67.14, 172.44),
                new DataPoint<double>(77.14, 183.94),
                new DataPoint<double>(79.34, 148.5),
                new DataPoint<double>(81.01, 128.16),
                new DataPoint<double>(84.01, 128.12),
                new DataPoint<double>(88.89, 152.59),
                new DataPoint<double>(92.19, 150.93),
                new DataPoint<double>(97.49, 169.45),
                new DataPoint<double>(103.58, 169.45),
                new DataPoint<double>(105.33, 133.61),
                new DataPoint<double>(106.58, 130.99),
                new DataPoint<double>(108.85, 152.37),
                new DataPoint<double>(110.14, 174.67),
                new DataPoint<double>(110.81, 119.26),
                new DataPoint<double>(115.03, 176.8),
                new DataPoint<double>(118.57, 144.85),
                new DataPoint<double>(12.57, 173.96),
                new DataPoint<double>(132.04, 135.31),
                new DataPoint<double>(137.21, 151.4),
                new DataPoint<double>(137.63, 168.57),
                new DataPoint<double>(140.23, 151.49),
                new DataPoint<double>(141.66, 127.9),
                new DataPoint<double>(144.3, 166.14),
                new DataPoint<double>(148.45, 183.29),
                new DataPoint<double>(150.97, 148.55),
                new DataPoint<double>(152.91, 147.5),
                new DataPoint<double>(16.86, 167.36),
                new DataPoint<double>(162.47, 160.81),
                new DataPoint<double>(168.46, 174.17),
                new DataPoint<double>(171.98, 177.67),
                new DataPoint<double>(175.48, 139.11),
                new DataPoint<double>(176.04, 152.65),
                new DataPoint<double>(183.28, 167.79)
            }),
            new Cluster<double> (new List<IDataPoint<double>> {
            new DataPoint<double>(2.72, 152.18),
            new DataPoint<double>(6.32, 90.63),
            new DataPoint<double>(10.38, 129.74),
            new DataPoint<double>(10.76, 111.43),
            new DataPoint<double>(21.41, 93.78),
            new DataPoint<double>(24.96, 115.39),
            new DataPoint<double>(25.16, 90.23),
            new DataPoint<double>(33.39, 103.99),
            new DataPoint<double>(35.51, 114.85),
            new DataPoint<double>(39.1, 83.23),
            new DataPoint<double>(39.2, 128.37),
            new DataPoint<double>(41.03, 121.53),
            new DataPoint<double>(44.57, 133.73),
            new DataPoint<double>(45.58, 112.88),
            new DataPoint<double>(57.19, 108.88),
            new DataPoint<double>(65.68, 107.67),
            new DataPoint<double>(67.19, 86.38),
            new DataPoint<double>(70.32, 104.77),
            new DataPoint<double>(75.65, 98.64),
            new DataPoint<double>(76.24, 88.68),
            new DataPoint<double>(76.48, 97.09),
            new DataPoint<double>(83.44, 109.81),
            new DataPoint<double>(84.21, 116.31),
            new DataPoint<double>(86.76, 112.6),
            new DataPoint<double>(89.78, 101.19),
            new DataPoint<double>(95.01, 89.07),
            new DataPoint<double>(96.39, 94.15),
            new DataPoint<double>(97.35, 111.27),
            new DataPoint<double>(99.37, 83.95),
            new DataPoint<double>(108.58, 103.72),
            new DataPoint<double>(110.69, 92.06),
            new DataPoint<double>(112.6, 83.13),
            new DataPoint<double>(12.35, 136.93),
            new DataPoint<double>(13.8, 122.44),
            new DataPoint<double>(13.96, 109.67),
            new DataPoint<double>(17.39, 110.04)
            }),
            new Cluster<double> (new List<IDataPoint<double>> {
            new DataPoint<double>(1.18, 54.39),
            new DataPoint<double>(3.76, 49.66),
            new DataPoint<double>(4.26, 32.58),
            new DataPoint<double>(4.27, 50.93),
            new DataPoint<double>(5.24, 48.12),
            new DataPoint<double>(7.8, 63.7),
            new DataPoint<double>(8.41, 55.34),
            new DataPoint<double>(9.56, 4.87),
            new DataPoint<double>(24.74, 20.64),
            new DataPoint<double>(26.15, 24.14),
            new DataPoint<double>(29.44, 11.83),
            new DataPoint<double>(30.11, 59.65),
            new DataPoint<double>(30.71, 50.06),
            new DataPoint<double>(31.18, 28.56),
            new DataPoint<double>(35.08, 68.08),
            new DataPoint<double>(35.25, 39.39),
            new DataPoint<double>(35.25, 1.34),
            new DataPoint<double>(37.57, 34.65),
            new DataPoint<double>(40.83, 25.54),
            new DataPoint<double>(44.33, 67.55),
            new DataPoint<double>(47.71, 5.82),
            new DataPoint<double>(49.54, 0.7),
            new DataPoint<double>(56.53, 6.35),
            new DataPoint<double>(57.64, 57.17),
            new DataPoint<double>(60.84, 55.32),
            new DataPoint<double>(64.27, 33.57),
            new DataPoint<double>(67.08, 20.22),
            new DataPoint<double>(70, 40.84),
            new DataPoint<double>(70.15, 75.43),
            new DataPoint<double>(72.59, 64.98),
            new DataPoint<double>(73.54, 69.04),
            new DataPoint<double>(76, 1.09),
            new DataPoint<double>(76.45, 60.16),
            new DataPoint<double>(76.53, 39.97),
            new DataPoint<double>(78.31, 77.04),
            new DataPoint<double>(82.79, 11.77),
            new DataPoint<double>(83.52, 53.42),
            new DataPoint<double>(84.79, 30.54),
            new DataPoint<double>(93.11, 68.63),
            new DataPoint<double>(94.55, 73.08),
            new DataPoint<double>(95.94, 48.55),
            new DataPoint<double>(96.45, 33.22),
            new DataPoint<double>(11.42, 48.11),
            new DataPoint<double>(11.49, 66.62),
            new DataPoint<double>(12.71, 66.26),
            new DataPoint<double>(14.55, 19.47),
            new DataPoint<double>(14.87, 7.94)
            }),
            new Cluster<double> (new List<IDataPoint<double>> {
            new DataPoint<double>(95.17, 3.07),
            new DataPoint<double>(101.13, 34),
            new DataPoint<double>(109.74, 1.27),
            new DataPoint<double>(111.07, 73.9),
            new DataPoint<double>(112.03, 73.23),
            new DataPoint<double>(112.41, 48.61),
            new DataPoint<double>(116.94, 80.88),
            new DataPoint<double>(117.16, 35.77),
            new DataPoint<double>(120.66, 27.13),
            new DataPoint<double>(121.81, 60.67),
            new DataPoint<double>(121.81, 25.9),
            new DataPoint<double>(125.58, 57.84),
            new DataPoint<double>(127.77, 59.05),
            new DataPoint<double>(128.34, 98.67),
            new DataPoint<double>(128.96, 74.36),
            new DataPoint<double>(130.26, 15.79),
            new DataPoint<double>(131.55, 36.07),
            new DataPoint<double>(133.8, 36.62),
            new DataPoint<double>(136.11, 5.78),
            new DataPoint<double>(136.19, 92.51),
            new DataPoint<double>(141.6, 79.39),
            new DataPoint<double>(146.22, 112.75),
            new DataPoint<double>(148.06, 3.49),
            new DataPoint<double>(148.98, 2.72),
            new DataPoint<double>(151.18, 1.27),
            new DataPoint<double>(151.36, 94.98),
            new DataPoint<double>(158.86, 90.28),
            new DataPoint<double>(159.69, 46.61),
            new DataPoint<double>(160.15, 51.14),
            new DataPoint<double>(162.53, 39.9),
            new DataPoint<double>(163.38, 46.09),
            new DataPoint<double>(164.01, 114.06),
            new DataPoint<double>(164.33, 4.66),
            new DataPoint<double>(165.16, 119.48),
            new DataPoint<double>(165.59, 100.3),
            new DataPoint<double>(168.01, 95.37),
            new DataPoint<double>(168.07, 71.73),
            new DataPoint<double>(168.8, 70.44),
            new DataPoint<double>(169.45, 40.66),
            new DataPoint<double>(170.74, 14.34),
            new DataPoint<double>(171.42, 19.23),
            new DataPoint<double>(172.2, 79.74),
            new DataPoint<double>(172.44, 89.47),
            new DataPoint<double>(173.66, 22.3),
            new DataPoint<double>(174.12, 32.57),
            new DataPoint<double>(174.49, 20.14),
            new DataPoint<double>(176.8, 95.03),
            new DataPoint<double>(177.92, 139.54),
            new DataPoint<double>(178.34, 114.58),
            new DataPoint<double>(181.05, 84.65),
            new DataPoint<double>(181.36, 74.5),
            new DataPoint<double>(183.58, 83.9),
            new DataPoint<double>(185.83, 129.32)
            })
        };

        foreach(Cluster<double> cluster in clusters)
        {
            yield return cluster;
        }
    }

    internal static IEnumerable<IDataPoint<double>> ReadOrGeneratePoints()
    {
        // Ange sökvägen för den lokala filen
        string filePath = "euklidiska_punkter.txt";

        List<IDataPoint<double>> dataPoints = new List<IDataPoint<double>>();

        // Om filen redan finns, läs in punkterna från filen
        if (File.Exists(filePath))
        {
            Console.WriteLine("Läser in befintliga punkter från filen...");

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Parse each line by extracting the x- and y-coordinates to create a data point. 
                    string[] parts = line.Split(';');
                    double x = double.Parse(parts[0]);
                    double y = double.Parse(parts[1]);
                    dataPoints.Add(new DataPoint<double>(x, y));
                }
            }
        }
        else
        {
            // Randomize the number of data points to be generated between 51 and 251. 
            Random rand = new Random();
            int numPoints = rand.Next(51, 251);

            // Generera the data points. 
            for (int i = 0; i < numPoints; i++)
            {
                double x = rand.NextDouble() * numPoints; // Random x-coordinat mellan 0 och number of points generated. 
                double y = rand.NextDouble() * numPoints; // Random x-coordinat mellan 0 och number of points generated. 
                dataPoints.Add(new DataPoint<double>(x, y));
            }

            // Write the new data points to the file. 
            using (StreamWriter writer = File.AppendText(filePath))
            {
                foreach (var dataPoint in dataPoints)
                {
                    writer.WriteLine(dataPoint);
                }
            }
        }

        foreach(IDataPoint<double> dataPoint in dataPoints)
        {
            yield return dataPoint;
        }
    }
}
