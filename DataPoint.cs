namespace GenericClustering;

/*

TODO:   Add support for modified data types by allowing users to define their own data classes which implements specific interfaces for
        the TSP problem. 

TODO:   Optimization of the distance calculations to ensure that they are effeive to various data types and distance metrics. Optimization may include using specialized algorithms for different distance metrics and to reduce the number of unnecessary calculate distance to improve performance. 

 */

/// <summary>
/// Represents a data point in a multi-dimensional space.
/// </summary>
/// <typeparam name="T">The type of the coordinates.</typeparam>
internal class DataPoint<T> : IDataPoint<T> where T: struct, IComparable<T>
{
    /// <summary>
    /// Delegate for defining a distance function between two arrays of type T representing coordinates.
    /// </summary>
    /// <typeparam name="T">The type of coordinates.</typeparam>
    public delegate double DistanceFunction(T[] coordinates, T[] otherCoordinates);

    /// <summary>
    /// Static field holding a distance function to calculate the distance between two arrays of coordinates.
    /// </summary>
    private static DistanceFunction distanceFunction;

    static DataPoint()
    {
        SetDistanceFunction(DistanceFunctions<T>.EuclideanDistance);
    }

    public T[] Coordinates { get; private set; }

    public T this[int index] => Coordinates[index];

    /// <summary>
    /// Initializes a new instance of the <see cref="DataPoint{T}"/> class with the specified coordinates.
    /// </summary>
    /// <param name="coordinates">The coordinates of the data point.</param>
    public DataPoint(params T[] coordinates)
    {
        Coordinates = coordinates;
    }

    /// <summary>
    /// Set the distance function to calculate the distance between two arrays of coordinates.
    /// </summary>
    /// <param name="distanceFunction">The function that will be used to calculate distance from now on.</param>
    public static void SetDistanceFunction(DistanceFunction distanceFunction)
    {
        DataPoint<T>.distanceFunction = distanceFunction;
    }

    /// <summary>
    /// Calculates the distance between this data point and another data point.
    /// </summary>
    /// <param name="otherPoint">The other data point.</param>
    /// <returns>The distance between the two data points.</returns>
    public double DistanceTo(IDataPoint<T> otherPoint)
    {
        if (Coordinates.Length != otherPoint.Coordinates.Length)
        {
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        return distanceFunction(Coordinates, otherPoint.Coordinates);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current data point.
    /// </summary>
    /// <param name="obj">The object to compare with the current data point.</param>
    /// <returns>True if the specified object is equal to the current data point; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var otherPoint = (DataPoint<T>)obj;

        if (Coordinates == null)
            return false;

        if (Coordinates.Length != otherPoint.Coordinates.Length)
            return false;

        for (int i = 0; i < Coordinates.Length; i++)
        {
            if (!Coordinates[i].Equals(otherPoint.Coordinates[i]))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Gets the hash code for the current data point.
    /// </summary>
    /// <returns>The hash code for the current data point.</returns>
    public override int GetHashCode()
    {
        // GetHashCode implementation for arrays is not suitable, as it doesn't consider array contents.
        // Consider using a custom hash code calculation for the array contents.
        return Coordinates.GetHashCode();
    }

    /// <summary>
    /// Gets a string representation of the current data point.
    /// </summary>
    /// <returns>The string representation for the current data point.</returns>
    public override string ToString()
    {
        return String.Join(';', Coordinates);
    }

    // TODO: Consider performance gain by implementing for specific types instead of using dynamic. 
    /// <summary>
    /// Implementation of IComparable<typeparamref name="T"/>
    /// </summary>
    /// <param name="other">The other data points to compare.</param>
    /// <returns>0 if the data points are equal, less than zero if this data points is smaller and greater than zero otherwise.</returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the data points do not match.</exception>
    public int CompareTo(DataPoint<T> other)
    {
        if (Coordinates.Count() != other.Coordinates.Count())
        { 
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        for (int i = 0; i < Coordinates.Count(); i++)
        {
            int comparisonResult = (dynamic)Coordinates[i] - other.Coordinates[i];
            if (comparisonResult != 0)
                return comparisonResult;
        }

        return 0; // All dimensions are equal
    }

    /// <summary>
    /// Implementation of IComparable<typeparamref name="T"/>
    /// </summary>
    /// <param name="other">The other data points to compare.</param>
    /// <returns>0 if the data points are equal, less than zero if this data points is smaller and greater than zero otherwise.</returns>
    public int CompareTo(T other)
    {
        return (dynamic)Coordinates[0] - other;
    }
}
