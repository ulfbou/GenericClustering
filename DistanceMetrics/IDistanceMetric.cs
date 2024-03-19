namespace GenericClustering;

internal interface IDistanceMetric<T> where T : struct
{
    T CalculateDistance();
}
