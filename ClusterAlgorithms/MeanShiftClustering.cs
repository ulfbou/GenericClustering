namespace GenericClustering.ClusterAlgorithms;

internal class MeanShiftClustering<T> where T : struct, IComparable<T>
{
    public MeanShiftClustering(List<IDataPoint<T>> dataPoints, double bandwidth) { }
    public void Cluster() { }
    public List<Cluster<T>> GetClusters()=> null;
}

