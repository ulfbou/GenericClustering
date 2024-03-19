namespace GenericClustering.ClusterAlgorithms;

/// <summary>
/// Implements the KMeans cluster algorithm for generic data types. 
/// </summary>
/// <typeparam name="T"></typeparam>
internal class KMeans<T> where T : struct, IComparable<T>
{
    private int K;                                  // The number of clusters used. 
    private List<IDataPoint<T>> DataPoints;         // A list of all data points. 
    private Cluster<T>[] Clusters;                  // An array to store all clusters. 
    private IDataPoint<T>[] Centroids;              // An Array to store all cluster's centroids. 
    private bool HasChanged = true;                 // Indicate whether the clusters have changed. 

    /// <summary>
    /// Initializes a new instance of <see cref="KMeans"/> class with given data points and amount of clusters to be used. 
    /// The clusters are randomly chosen from the data points. 
    /// </summary>
    /// <param name="dataPoints">The data points are to be clustered.</param>
    /// <param name="k">The amount of clusters to be used.</param>
    /// <exception cref="ArgumentNullException">Thrown if the list of data points are null.</exception>
    /// <exception cref="ArgumentException">Thrown if the set of data points is empty, if the number of 
    /// clusters are set to less than two or greater than or equal to the number of data points.</exception>
    public KMeans(List<IDataPoint<T>> dataPoints, int k)
    {
        K = k;
        DataPoints = dataPoints ?? throw new ArgumentNullException(nameof(dataPoints));

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

        Clusters = new Cluster<T>[k];
        Centroids = new DataPoint<T>[k];

        Centroids = SelectRandomPoints(DataPoints).ToArray();

        for (int i = 0; i < Centroids.Length; i++)
        {
            Clusters[i] = new Cluster<T>(Centroids[i]);
        }
    }

    /// <summary>
    /// Gets or sets the number of clusters. 
    /// </summary>
    public int NumberOfClusters { get => K; internal set => K = value; }

    public void Run(int maxIterations = 100)
    {
        // Safeguard: Limit the number of iterations to prevent convergence to a local optimum. 
        int iterations = 0;

        while (iterations < maxIterations && !HasConverged())
        {
            iterations++;

            AssignDataToClusters();
        }
    }

    /// <summary>
    /// Assign data points to the cluster with the closest centroid. 
    /// </summary>
    private void AssignDataToClusters()
    {
        // Assume the centroids hasn't changed during this iteration. 
        HasChanged = false;

        // A list of data points for each cluster. 
        List<IDataPoint<T>>[] clusterPoints = new List<IDataPoint<T>>[NumberOfClusters];

        // Reset the new updated set of DataPoints for each Cluster. 
        for (int i = 0; i < NumberOfClusters; i++)
        {
            clusterPoints[i] = new List<IDataPoint<T>>();
        }

        foreach (var point in DataPoints)
        {
            // Find the closest Cluster for each data point.
            int closestId = Enumerable.Range(0, NumberOfClusters)
                .Aggregate((minIndex, j) =>
                    Centroids[minIndex].DistanceTo(point) < Centroids[j].DistanceTo(point) ? minIndex : j);

            clusterPoints[closestId].Add(point);
        }

        // For each Cluster, compare the new DataPoints with the old ones. 
        // If we find a new sequence, the clusters have changed and, thus, we create a new Cluster. 
        for (int i = 0; i < NumberOfClusters; i++)
        {
            if (!Clusters[i].GetAllDataPoints().SequenceEqual(clusterPoints[i]))
            {
                HasChanged = true;
                Clusters[i] = new Cluster<T>(clusterPoints[i]);
            }
        }
    }

    /// <summary>
    /// Checks if the clustering has converged. 
    /// </summary>
    /// <returns>true if the clustering has converged; otherwise false.</returns>
    private bool HasConverged() => !HasChanged;

    /// <summary>
    /// Randomly selects K unique elements from the given list of data points. 
    /// </summary>
    /// <param name="dataPoints">A list of data points.</param>
    /// <returns>A list with randomly chosen data points. </returns>
    private List<IDataPoint<T>> SelectRandomPoints(List<IDataPoint<T>> dataPoints)
    {
        if (dataPoints == null)
        {
            throw new ArgumentNullException(nameof(dataPoints));
        }

        // Implementation of Fisher-Yates-shuffling.
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

    /// <summary>
    /// Gets all formed clusters. 
    /// </summary>
    /// <returns>A list of clusters.</returns>
    public IEnumerable<Cluster<T>> GetAllClusters()
    {
        if (!HasConverged())
        {
            throw new InvalidOperationException("The clustering has not converged yet.");
        }

        return Clusters.ToList();
    }
}
