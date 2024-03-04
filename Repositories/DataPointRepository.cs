using GenericClustering;

namespace GenericClustering.Repositories;

public class DataPointRepository<T>
{
    List<DataPoint<T>> DataPoints = new();

    public void Add(DataPoint<T> dataPoint)
    {
        DataPoints.Add(dataPoint);
    }

    public void Save()
    {
         foreach (var dataPoint in DataPoints)
        {
            Console.WriteLine(dataPoint);
        }
    }
}

