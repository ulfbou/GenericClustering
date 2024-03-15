namespace GenericClustering;

internal interface IDataPoint<T> where T :struct
{
    T[] Coordinates => throw new NotImplementedException();

    /// <summary>
    /// Calculates the distance between this data point and another data point.
    /// </summary>
    /// <param name="otherPoint">The other data point.</param>
    /// <returns>The distance between the two data points.</returns>
    double DistanceTo(IDataPoint<T> otherPoint);
}