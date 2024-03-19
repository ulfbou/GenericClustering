namespace GenericClustering.CoefficientCalculators;


/*
Använd IEnumerable istället för List i metoder: När det är möjligt, använd IEnumerable istället för List i metodsignaturer för att göra 
koden mer flexibel och minska beroendet av specifika samlingstyper.

Optimera beräkningen av centroidavstånd: I metoden CalculateAverageDistanceToOtherClusters beräknas avståndet till varje klustercentrum 
för varje datapunkt. Du kan optimera detta genom att undvika att beräkna avståndet till det egna klustret (eftersom det redan är känt) 
och genom att använda en effektivare algoritm för att beräkna avstånden.

Implementera en konvergenskontroll för KMeans: Lägg till en konvergenskontroll i KMeans-algoritmen för att avsluta loopen när klustren 
inte längre ändras betydligt mellan iterationerna. Detta skulle förbättra prestandan genom att minska antalet onödiga iterationer.
*/

/// <summary>
/// Class that calculates the Silhouette coefficient for a set of clusters. 
/// </summary>
/// <typeparam name="T">The type of data points in the clusters.</typeparam>
/// <typeparam name="T">Typen av datapunkter i klustren.</typeparam>
internal class SilhouetteCalculator<T> where T : struct, IComparable<T>
{
    private readonly Cluster<T>[] Clusters;                         // An array of clusters.
    private readonly IDataPoint<T>[] DataPoints;                    // An array of data points. 
    private readonly double[,] DistanceMatrix;                      // A matrix containing the distances between data points. 
    private readonly Dictionary<IDataPoint<T>, int> DataPointIndex; // A map between data points and their indice in the DataPoints array.

    /// <summary>
    /// The constructor for the SilhouetteCalculate class. 
    /// </summary>
    /// <param name="clusters">An IEnumerable of clusters that are to be evaluated.</param>
    public SilhouetteCalculator(IEnumerable<Cluster<T>> clusters)
    {
        // Convert the argument of IEnumerable of clusters to an array and storing it in Clusters. 
        Clusters = clusters.ToArray();

        // Collect the data points from all clusters and store them in one single array. 
        DataPoints = Clusters.SelectMany(cluster => cluster.GetAllDataPoints()).ToArray();

        // Calculate the distance matrix. 
        DistanceMatrix = CalculateDistanceMatrix(DataPoints);

        // Create a map between data points and their indice in the DataPoints array. 
        DataPointIndex = CalculateDataPointIndex(DataPoints);
    }

    /// <summary>
    /// A method to create a map between data points and their indice in the DataPoints array. 
    /// </summary>
    /// <param name="dataPoints">An array of data points.</param>
    /// <returns>A map between the data points and their indice.</returns>
    private Dictionary<IDataPoint<T>, int> CalculateDataPointIndex(IDataPoint<T>[] dataPoints)
    {
        return dataPoints.Select((point, index) => new { Point = point, Index = index }).
            ToDictionary(pair => pair.Point, pair => pair.Index);
    }

    /// <summary>
    /// A method to calculate the distance matrix between all data points. 
    /// </summary>
    /// <param name="dataPoints">An array of data points.</param>
    /// <returns>A distance matrix between all data points.</returns>
    private double[,] CalculateDistanceMatrix(IDataPoint<T>[] dataPoints)
    {
        int n = dataPoints.Length;
        double[,] distanceMatrix = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // Calculate the distance between data point i and data point j, storing it in the matrix. 
                double distance = dataPoints[i].DistanceTo(dataPoints[j]);
                distanceMatrix[i, j] = distance;
                distanceMatrix[j, i] = distance;
            }
        }

        return distanceMatrix;
    }

    /// <summary>
    /// A method to evaluate the Silhouette coefficient on the current set of clusters.
    /// </summary>
    /// <returns>The average Silhouette Coefficient for all clusters.</returns>
    public double Evaluate()
    {
        double silhouetteSum = 0;

        for (int i = 0; i < Clusters.Length; i++)
        {
            double a = CalculateAverageDistance(Clusters[i], Clusters[i]);
            double b = double.MaxValue;

            // Find the minimum distance to another cluster from cluster i
            for (int j = 0; j < Clusters.Length; j++)
            {
                if (i != j)
                {
                    double distance = CalculateAverageDistance(Clusters[i], Clusters[j]);
                    b = Math.Min(b, distance);
                }
            }

            // Set the silhouette to zero to avoid division by zero.
            double silhouette = (a == b) ? 0 : (b - a) / Math.Max(a, b);
            silhouetteSum += silhouette;
        }

        return silhouetteSum / Clusters.Length;
    }

    /// <summary>
    /// A method to calculate the average distance between two clusters.
    /// </summary>
    /// <param name="clusterA">The first cluster.</param>
    /// <param name="clusterB">The second cluster.</param>
    /// <returns>The average distance between cluster A and cluster B.</returns>
    private double CalculateAverageDistance(Cluster<T> clusterA, Cluster<T> clusterB)
    {
        double sum = 0;
        int count = 0;

        foreach (var dataPointA in clusterA.GetAllDataPoints())
        {
            // Look up the indice of data point A.
            int indexA = DataPointIndex[dataPointA];

            foreach (var dataPointB in clusterB.GetAllDataPoints())
            {
                // Look up the indice of data point B.
                int indexB = DataPointIndex[dataPointB];
                sum += DistanceMatrix[indexA, indexB];
                count++;
            }
        }

        // Calculate the average distance by dividing the sum with the number of data points. 
        return sum / count;
    }
}
