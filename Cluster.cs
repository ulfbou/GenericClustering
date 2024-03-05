using System;
using System.Collections.Generic;
using System.Linq;
using GenericClustering.Entities;

namespace GenericClustering;

public class Cluster<T> where T : struct
{
    public List<DataPoint<T>> DataPoints { get; private set; }
    public DataPoint<T> Centroid { get; private set; }
    public double MeanDistance { get; private set; }
    public double StandardDeviation { get; private set; }
    
    // Construct a new Cluster with a centroid and empty datapoints. 
    public Cluster(DataPoint<T> centroid)
    {
        DataPoints = new List<DataPoint<T>>();
        Centroid = centroid ?? throw new ArgumentNullException(nameof(centroid));
    }

    // Construct a new Cluster from existing datapoints. Calculate the centroid from the datapoints. 
    public Cluster(List<DataPoint<T>> dataPoints)
    {
        DataPoints = new List<DataPoint<T>>(dataPoints);
        Centroid = new DataPoint<T>();
        
        Update();
    }

    // Uppdatera mittpunkten
    private void Update()
    {
        if (DataPoints.Count == 0)
            return;

        int dimensions = DataPoints[0].Coordinates.Length;
        double[] totalCoordinates = new double[dimensions];

        foreach (var point in DataPoints)
        {
            for (int i = 0; i < dimensions; i++)
            {
                totalCoordinates[i] += Convert.ToDouble(point.Coordinates[i]);
            }
        }

        // Calculate the new centroid.
        var centroidCoordinates = ConvertToT(totalCoordinates.Select(total => total / DataPoints.Count).ToArray());

        Centroid = new DataPoint<T>(centroidCoordinates);

        // Calculate the distance to the centroid.
        List<double> distances = DataPoints.Select(point => Centroid.DistanceTo(point)).ToList();

        // Calculate the mean distance. 
        MeanDistance = distances.Sum() / distances.Count;

        // Calculate the standard deviation. 
        double sumOfSquaredDifferences = distances.Select(d => Math.Pow(d - MeanDistance, 2)).Sum();
        StandardDeviation = Math.Sqrt(sumOfSquaredDifferences / distances.Count);
    }

    private T ConvertToT(double[] coordinates)
    {
        // Implementera logik för att omvandla double[] till T.
        return (T)(object)coordinates;
    }


    public void Add(DataPoint<T> point)
    {
        DataPoints.Add(point);
        Update();
    }

    public void Add(List<DataPoint<T>> dataPoints)
    {
        DataPoints.AddRange(dataPoints);
        Update();
    }

    public double DistanceTo(DataPoint<T> point)
    {
        return Centroid.DistanceTo(point);
    }
}

/*
public class KMeans<T> where T : DataPoint<T>
{
    private List<Cluster<T>> Clusters;
    private List<DataPoint<T>> DataPoints;

    public KMeans(List<DataPoint<T>> dataPoints)
    {
        DataPoints = dataPoints;
        Clusters = new List<Cluster<T>>();
    }

    public void InitializeClusters(int k)
    {
        // Implementera initialiseringslogik för k kluster
        // Exempelvis slumpmässig placering av centroider
    }

    public void AssignDataToClusters()
    {
        // Tilldela varje datapunkt till närmaste kluster
        // Använd avståndsmått (t.ex. euklidiskt avstånd)
    }

    public void UpdateClusterCentroids()
    {
        // Uppdatera centroider för varje kluster
    }

    public bool HasConverged()
    {
        // Kontrollera om algoritmen har konvergerat
        // Exempelvis genom att jämföra gamla och nya centroider
    }

    public List<Cluster<T>> GetClusters()
    {
        return Clusters;
    }
}
*/