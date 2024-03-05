namespace GenericClustering.Entities;

public class DataPoint<T>
{
    public T[] Coordinates { get; private set; }

    public DataPoint(params T[] coordinates) => Coordinates = coordinates;

    public double DistanceTo(DataPoint<T> otherPoint)
    {
        if (Coordinates.Length != otherPoint.Coordinates.Length)
            throw new ArgumentException("Data points must have the same number of dimensions.");

        double sumOfSquaredDifferences = Coordinates.Zip(otherPoint.Coordinates, (x, y) =>
        {
            double difference = Convert.ToDouble(x) - Convert.ToDouble(y);
            return difference * difference;
        }).Sum();

        return Math.Sqrt(sumOfSquaredDifferences);
    }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var otherPoint = (DataPoint<T>)obj;

        if (Coordinates == null)
        {
            return false;
        }

        if (Coordinates.Length != otherPoint.Coordinates.Length)
            return false;

        for (int i = 0; i < Coordinates.Length; i++)
        {
            if (!Coordinates[i].Equals(otherPoint.Coordinates[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return Coordinates.GetHashCode();
    }
}

public class EuclideanNode : DataPoint<double>
{
    public EuclideanNode(params double[] coordinates) : base(coordinates) {}
}

public class GeoDataNode : DataPoint<double>
{
    public GeoDataNode(double latitude, double longitude) : base(latitude, longitude) {}
}
