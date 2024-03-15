using System.Diagnostics.Metrics;
using System.Numerics;

namespace GenericClustering;

internal class SilhouetteCoefficient<T> where T : struct
{
    private int k = 2;
    private double bestScore = 0;
    private Cluster<T>[] bestClustering;
    public IDataPoint<T>[] DataPoints;

    public SilhouetteCoefficient(IEnumerable<DataPoint<T>> dataPoints)
    {
        DataPoints = dataPoints.ToArray();
    }

    public double Evaluate()
    {
        List<IDataPoint<T>> dataPoints = DataPoints.ToList();
        KMeans<T> kMeans = new KMeans<T>(dataPoints, k);

        // Safeguard: Limit the number of iterations to prevent convergence to a local optimum. 
        int maxIterations = 100;
        int iterations = 0;

        kMeans.InitializeClusters();

        while (iterations < maxIterations && !kMeans.HasConverged())
        {
            iterations++;

            kMeans.AssignDataToClusters();
        }

        List<Cluster<T>> allClusters = kMeans.GetAllClusters().ToList();

        double totalSC = 0;

        foreach (var cluster in allClusters)
        {
            double SC = CalculateSilhouetteCoefficient(cluster, allClusters, dataPoints);
            totalSC += SC;
        }

        double averageSC = totalSC / allClusters.Count();

        if (bestScore < averageSC)
        {
            bestScore = averageSC;
            bestClustering = allClusters.ToArray();
        }

        return averageSC;
    }

    // Calculate the Silhouette Coefficient (SC).
    private double CalculateSilhouetteCoefficient(
        Cluster<T> cluster,                 // The cluster which we calculate the SC from.
        List<Cluster<T>> allClusters,       // All clusters formed. 
        List<IDataPoint<T>> allDataPoints)   // All data points collectively. 
    {
        if (cluster == null)
        {
            throw new ArgumentNullException(nameof(cluster));
        }

        if (allClusters == null)
        {
            throw new ArgumentNullException(nameof(allClusters));
        }

        if (allDataPoints == null)
        {
            throw new ArgumentNullException(nameof(allDataPoints));
        }

        if (cluster.DataPoints.Count() == 0)
        {
            throw new ArgumentException("Empty cluster.");
        }

        if (allClusters.Count() == 0)
        {
            throw new ArgumentException("List of clusters is empty.");
        }

        if (allDataPoints.Count() == 0)
        {
            throw new ArgumentException("List of data points is empty.");
        }

        List<IDataPoint<T>> clusterDataPoints = cluster.DataPoints;
        double totalSC = 0;

        foreach (var dataPoint in clusterDataPoints)
        {
            // Calculate the distance from the data point to its cluster's centroid. 
            var a = dataPoint.DistanceTo(cluster.Centroid);

            // Calculate the average distance from the data point to the centroid of the other clusters. 
            var b = CalculateAverageDistanceToOtherClusters(dataPoint, cluster, allClusters);

            // Calculate the SC for this data point. 
            double max = Math.Max(a, b);            // Avoid dividing by zero. 
            double SC = max != 0 ? (b - a) / max : 0;

            // Add the silhouetteCoefficient to the totalSilhouetteCoefficient. 
            totalSC += SC;
        }

        // Calculate the average SC for all data points. 
        return totalSC / allDataPoints.Count();
    }

    // Calculate the average distance from a data point to all clusters except its own cluster. 
    private static double CalculateAverageDistanceToOtherClusters(IDataPoint<T> dataPoint, Cluster<T> cluster, List<Cluster<T>> allClusters)
    {
        return allClusters.Sum(
            otherCluster => cluster != otherCluster ? dataPoint.DistanceTo(otherCluster.Centroid) : 0);
    }

    public List<Cluster<T>> GetBestSC() => bestClustering.ToList();
}
