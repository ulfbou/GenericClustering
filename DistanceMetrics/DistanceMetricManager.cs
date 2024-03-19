
namespace GenericClustering;


/*
internal class EuclideanDistance<T> : IDistanceMetric<T>
{
    double CalculateDistance(T[] p1, T[] p2);
}

    IDistanceMetric<T> distanceMetric = DistanceMetricManager<T>.getCurrentDistanceMetric();
    DistanceMetricManager<T>.SetDistanceMetric(new IDistanceMetric<T> ());

    T distance = distanceMetric.CalculateDistance();

 */
internal static class DistanceMetricManager<T> where T : struct
{
    private static IDistanceMetric<T> currentDistanceMetric;

    static DistanceMetricManager()
    {
        //SetDistanceMetric(new EuclideanDistance<T> ());
    }

    internal static IDistanceMetric<T> getCurrentDistanceMetric()
    {
        return currentDistanceMetric;
    }

    internal static void SetDistanceMetric(IDistanceMetric<T> distanceMetric)
    {
        currentDistanceMetric = distanceMetric;
    }
}