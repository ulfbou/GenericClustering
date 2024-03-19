namespace GenericClustering;

internal interface IDataPoint<T> : IComparable<T> where T :struct, IComparable<T>
{
    T[] Coordinates => throw new NotImplementedException();

    /// <summary>
    /// Calculates the distance between this data point and another data point.
    /// </summary>
    /// <param name="otherPoint">The other data point.</param>
    /// <returns>The distance between the two data points.</returns>
    double DistanceTo(IDataPoint<T> otherPoint);

    /// <summary>
    /// Gets the coordinate at index position.
    /// </summary>
    /// <param name="index">The index of the coordinate.</param>
    /// <returns></returns>
    T this[int index] { get;  }
}
