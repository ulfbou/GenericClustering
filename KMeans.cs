﻿using System.Collections;
using System.Collections.Generic;

namespace GenericClustering;

public class KMeans<T>(List<DataPoint<T>> dataPoints, int k) where T: struct
{
    private int K = k;
    private List<DataPoint<T>> DataPoints = dataPoints ?? throw new ArgumentNullException(nameof(dataPoints));
    private Cluster<T>[] Clusters = new Cluster<T>[k];
    private List<DataPoint<T>>[] ClusterPoints = new List<DataPoint<T>>[k];
    private DataPoint<T>[] Centroids = new DataPoint<T>[k];
    private bool HasChanged = true;

    // Initialize the clusters by chosing random points as centroids
    public void InitializeClusters()
    {
        if (DataPoints.Count == 0)
        {
            throw new ArgumentException("Datapoints are empty");
        }
            
        if (K < 2)
        {
            throw new ArgumentException("Cannot cluster less than 2 clusters.");
        }

        if (K >= DataPoints.Count)
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
        for (int i = 0; i < K; i++)
        {
            ClusterPoints[i] = new List<DataPoint<T>>();
        }

        // Find the closest Cluster for each element in DataPoints. 
        foreach (var point in DataPoints)
        {
            int closestId = Enumerable.Range(0, k)
                .Aggregate((minIndex, j) =>
                    Centroids[minIndex].DistanceTo(point) < Centroids[j].DistanceTo(point) ? minIndex : j);

            ClusterPoints[closestId].Add(point);
        }

        // For each Cluster, compare the new DataPoints with the old ones. 
        // If we find a new sequence, the clusters have changed. 
        // Finally, create new Clusters. 
        for (int i = 0; i < K; i++)
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
    private List<DataPoint<T>> SelectRandomPoints(List<DataPoint<T>> dataPoints)
    {
        Random rng = new Random();
        List<DataPoint<T>> selectedPoints = new List<DataPoint<T>>(K);      // Chosen datapoints
        HashSet<int> selectedIndices = new HashSet<int>();                  // Indice of selected datapoints

        while (selectedPoints.Count < K)
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
}

/*
public class KMeans<T>(List<DataPoint<T>> dataPoints) where T : DataPoint<T>
{
    private readonly List<DataPoint<T>> dataPoints = dataPoints ?? throw new ArgumentNullException(nameof(dataPoints));
    private Cluster<T>[] clusters;
    private List<DataPoint<T>>[] clusterPoints;
    private bool hasChanged = true;


    public void InitializeClusters(int k)
    {
        if (k < 2)
            throw new ArgumentException("Less than 2 clusters.");

        clusters = new Cluster<T>[k];
        clusterPoints = new List<DataPoint<T>>[k];
        DataPoint<T>[] centroids = SelectRandomPoints(dataPoints, k).ToArray();

        // Initialize clusters with centroids
        for (int i = 0; i < k; i++)
            clusters[i] = new Cluster<T>(centroids[i]);
    }

    public void AssignDataToClusters()
    {
        for (int i = 0; i < k; i++)
            clusterPoints[i] = new List<DataPoint<T>>();

        foreach (var point in dataPoints)
        {
            int closestClusterId = clusters
                .Select((cluster, index) => (index, distance: cluster.Centroid.DistanceTo(point)))
                .MinBy(pair => pair.distance)
                .index;

            clusterPoints[closestClusterId].Add(point);
        }
    }

    public void UpdateClusterCentroids()
    {
        hasChanged = false;


    }

    public bool HasConverged()
    {
        return !hasChanged;
    }

    public List<Cluster<T>> GetClusters()
    {
        return clusters.ToList();
    }

    private List<DataPoint<T>> SelectRandomPoints(List<DataPoint<T>> points, int k)
    {
        Random random = new Random();
        List<DataPoint<T>> shuffledPoints = points.OrderBy(p => random.Next()).ToList();
        return shuffledPoints.Take(k).ToList();
    }
}
*/