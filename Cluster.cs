namespace GenericClustering;

/// <summary>
/// Represents a cluster of data points in multidimensional space. 
/// </summary>
/// <typeparam name="T">The type of coordinates for the data points.</typeparam>
internal class Cluster<T> where T : struct, IComparable<T>
{
    /// <summary>
    /// Gets the list of data points which belongs to this cluster.
    /// </summary>
    private List<IDataPoint<T>> DataPoints { get; set; }

    /// <summary>
    /// Gets the centroid for this cluster. 
    /// </summary>
    public IDataPoint<T> Centroid { get; private set; }

    /// <summary>
    /// Gets the mean distance from the data points to the centroid of this cluster.
    /// </summary>
    public double MeanDistance { get; private set; }

    /// <summary>
    /// Gets the standard deviation of the distances from the data points to the centroid of this cluster.
    /// </summary>
    public double StandardDeviation { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="Cluster{T}"/> class with a specified centroid and empty data points. 
    /// </summary>
    /// <param name="centroid">The centroid of the cluster.</param>
    public Cluster(IDataPoint<T> centroid)
    {
        DataPoints = new List<IDataPoint<T>>();
        Centroid = centroid ?? throw new ArgumentNullException(nameof(centroid));
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Cluster{T}"/> class from existing data points and calculates the centroid.
    /// </summary>
    /// <param name="dataPoints">The list of data points.</param>
    public Cluster(List<IDataPoint<T>> dataPoints)
    {
        DataPoints = new List<IDataPoint<T>>(dataPoints);
        Centroid = new DataPoint<T>();

        Update();
    }

    /// <summary>
    /// Updates the centroid and cluster statistics. 
    /// </summary>
    private void Update()
    {
        if (DataPoints.Count() == 0)
        {
            return;
        }

        int dimensions = DataPoints[0].Coordinates.Length;
        double[] totalCoordinates = new double[dimensions];

        foreach (var point in DataPoints)
        {
            for (int i = 0; i < dimensions; i++)
            {
                totalCoordinates[i] += (dynamic)point.Coordinates[i];
            }
        }

        // Calculates the new centroid.
        var centroidCoordinates = (dynamic)(totalCoordinates.Select(total => total / DataPoints.Count).ToArray());

        Centroid = new DataPoint<T>(centroidCoordinates);

        // Calculate the distances to the centroid.
        double[] distances = DataPoints.Select(dataPoint => Centroid.DistanceTo(dataPoint)).ToArray();

        // Calculate the mean average. 
        MeanDistance = (distances.Sum() / distances.Length);

        // Calculate the standard deviation. 
        double sumOfSquaredDifferences = distances.Select(d => Math.Pow(d - MeanDistance, 2)).Sum();
        StandardDeviation = Math.Sqrt(sumOfSquaredDifferences / distances.Length);
    }

    /// <summary>
    /// Calculates the distance from the centroid to another data point.
    /// </summary>
    /// <param name="point">The other point to calculate the distance to.</param>
    /// <returns>The distance between this cluster's centroid and the other data point.</returns>
    public double DistanceTo(DataPoint<T> point)
    {
        return Centroid.DistanceTo(point);
    }

    /// <summary>
    /// Gets all data points within this cluster.
    /// </summary>
    /// <returns>An IEnumerable of the data points.</returns>
    public IEnumerable<IDataPoint<T>> GetAllDataPoints() => DataPoints;
}
