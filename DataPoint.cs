namespace GenericClustering;

/*

TODO: Implementera en gränssnittsbaserad lösning: Skapa ett gränssnitt som definierar de grundläggande funktionerna som krävs för att hantera datapunkter i TSP-problemet, inklusive att beräkna avståndet mellan två datapunkter och jämföra dem. Sedan kan DataPoint<T> implementera detta gränssnitt, vilket gör det möjligt att skapa specialiserade versioner för olika datatyper.

TODO: Tillåt anpassad datahantering: Lägg till stöd för anpassade datatyper genom att tillåta användare att definiera sina egna dataklasser som implementerar ett specifikt gränssnitt för TSP-problemet. Detta ger maximal flexibilitet och gör det möjligt för användaren att hantera olika datatyper och avståndsfunktioner enligt deras behov.

TODO: Optimera prestanda: Se över implementeringen av avståndsberäkningsmetoderna för att säkerställa att de är effektiva för olika typer av datatyper och avståndsfunktioner. Optimeringar kan inkludera att använda specialiserade algoritmer för olika avståndsmått och att minska onödiga beräkningar för att förbättra prestanda.


 */

/// <summary>
/// Represents a data point in a multi-dimensional space.
/// </summary>
/// <typeparam name="T">The type of the coordinates.</typeparam>
internal class DataPoint<T> : IDataPoint<T> where T: struct
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
}

internal class DoubleDataPoint : DataPoint<double> { }