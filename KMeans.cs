using GenericClustering;
using System.Collections;
using System.Collections.Generic;

namespace GenericClustering;

internal class KMeans<T> where T: struct
{
    private int K;
    private List<IDataPoint<T>> DataPoints;
    private Cluster<T>[] Clusters;
    private List<IDataPoint<T>>[] ClusterPoints;
    private IDataPoint<T>[] Centroids;
    private bool HasChanged = true;

    public KMeans(List<IDataPoint<T>> dataPoints, int k)
    {
        K = k;
        DataPoints = dataPoints ?? throw new ArgumentNullException(nameof(dataPoints));
        Clusters = new Cluster<T>[k];
        Centroids = new DataPoint<T>[k];
    }

    public int NumberOfClusters { get => K; set => K = value; }

    // Initialize the clusters by chosing random points as centroids
    public void InitializeClusters()
    {
        if (DataPoints.Count == 0)
        {
            throw new ArgumentException("Datapoints are empty");
        }
            
        if (NumberOfClusters < 2)
        {
            throw new ArgumentException("Cannot cluster less than 2 clusters.");
        }

        if (NumberOfClusters >= DataPoints.Count)
        {
            throw new ArgumentException("Cannot cluster more or equal number of clusters than given datapoints.");
        }

        Centroids = SelectRandomPoints(DataPoints).ToArray();
    }

    public void AssignDataToClusters()
    {
        // Assume the centroids hasn't changed during this iteration. 
        HasChanged = false;

        // Reset the new updated set of DataPoints for each Cluster. 
        for (int i = 0; i < NumberOfClusters; i++)
        {
            ClusterPoints[i] = new List<IDataPoint<T>>();
        }

        foreach (var point in DataPoints)
        {
            // Find the closest Cluster for each data point.
            int closestId = Enumerable.Range(0, NumberOfClusters)
                .Aggregate((minIndex, j) =>
                    Centroids[minIndex].DistanceTo(point) < Centroids[j].DistanceTo(point) ? minIndex : j);

            ClusterPoints[closestId].Add(point);
        }

        // For each Cluster, compare the new DataPoints with the old ones. 
        // If we find a new sequence, the clusters have changed. 
        // Finally, create new Clusters. 
        for (int i = 0; i < NumberOfClusters; i++)
        {
            if (!Clusters[i].DataPoints.SequenceEqual(ClusterPoints[i]))
            {
                HasChanged = true;
                Clusters[i] = new Cluster<T>(ClusterPoints[i]);
            }
        }
    }

    public bool HasConverged() => !HasChanged;

    // Select K random unique elements. 
    // Implementation of Fisher-Yates-shuffling.
    private List<IDataPoint<T>> SelectRandomPoints(List<IDataPoint<T>> dataPoints)
    {
        Random rng = new Random();
        List<IDataPoint<T>> selectedPoints = new List<IDataPoint<T>>(NumberOfClusters);      // Chosen datapoints
        HashSet<int> selectedIndices = new HashSet<int>();                  // Indice of selected datapoints

        while (selectedPoints.Count < NumberOfClusters)
        {
            int randomIndex = rng.Next(dataPoints.Count);

            if (!selectedIndices.Contains(randomIndex))
            {
                selectedPoints.Add(dataPoints[randomIndex]);
                selectedIndices.Add(randomIndex);
            }
        }

        return selectedPoints;
    }

    internal List<Cluster<T>> GetAllClusters() => Clusters.ToList();
}
