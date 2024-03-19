namespace GenericClustering;

internal class GaussianMixtureModel<T> where T : struct, IComparable<T>
{
    public GaussianMixtureModel(List<IDataPoint<T>> dataPoints, int numComponents) { }
    public void Train() { }
    public List<Cluster<T>> GetClusters() => null;
}
