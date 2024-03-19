namespace GenericClustering;

public static class DistanceFunctions<T>
{
    /// <summary>
    /// Calculates the Euclidean distance. 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the points differ.</exception>
    public static double EuclideanDistance(T[] point1, T[] point2)
    {
        if (point1.Length != point2.Length)
        {
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        double sumOfSquaredDifferences = 0;

        for (int i = 0; i < point1.Length; i++)
        {
            double difference = (dynamic)point1[i] - point2[i];
            sumOfSquaredDifferences += difference * difference;
        }

        return Math.Sqrt(sumOfSquaredDifferences);
    }

    /// <summary>
    /// Calculates the Manhattan distance (taxi cab distance), which measure the absolute distance in each dimension.  
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the points differ.</exception>
    public static double ManhattanDistance(double[] point1, double[] point2)
    {
        if (point1.Length != point2.Length)
        {
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        double sumOfAbsoluteDifferences = 0;

        for (int i = 0; i < point1.Length; i++)
        {
            double difference = (dynamic)point1[i] - point2[i];
            sumOfAbsoluteDifferences += Math.Abs(difference);
        }

        return Math.Sqrt(sumOfAbsoluteDifferences);
    }

    /// <summary>
    /// Calculates the Chebshev distance between two vectors in a room, where the distance is the largest difference in each dimension. 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the points differ.</exception>
    public static double ChebshevDistance(T[] point1, T[] point2)
    {
        if (point1.Length != point2.Length)
        {
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        // Formel: max(abs(x_i-y_i))
        double maxDifference = 0;

        for (int i = 0; i < point1.Length; i++)
        {
            double difference = (dynamic)point1[i] - point2[i];
            maxDifference = Math.Max((dynamic)difference, (dynamic)maxDifference);
        }

        return maxDifference;
    }

    /// <summary>
    /// Calculates the Cosine similarity, which measures the similarity between two vectors by calculate the cosinus of the angle 
    /// between them. 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <returns>The Cosine Similarity</returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the points differ or are not equal.</exception>
    public static double CosineSimilarityDistance(
        T[] point1,
        T[] point2)
    {
        if (point1.Length != point2.Length)
        {
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        // Formel: A* B/(sqrt(sum((a_i* a_i)))*sqrt(sum((b_i* b_i))))

        double dotProduct = 0;
        double norm1 = 0;
        double norm2 = 0;

        for (int i = 0; i < point1.Length; i++)
        {
            dotProduct += (dynamic)point1[i] * point2[i];
            norm1 += (dynamic)point1[i] * point1[i];
            norm2 += (dynamic)point2[i] * point2[i];
        }

        norm1 = Math.Sqrt(norm1);
        norm2 = Math.Sqrt(norm2);

        // Handle division by zero edge case
        if (norm1 == 0 || norm2 == 0)
        {
            return 0;
        }

        return dotProduct / (norm1 * norm2);
    }

    /// <summary>
    /// Calculates the Great Circle Distance between two points on a sphere accounting for the curvature of the sphere. 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <param name="radius">The radius of the great circle.</param>
    /// <returns>The distance between the given data points.</returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the points differ.</exception>
    public static double GreatCircleDistance(double[] point1, double[] point2, double radius)
    {
        if (point1.Length != 2 && point2.Length != 2)
        {
            throw new ArgumentException("Data points must have two dimensions.");
        }

        // Convert latitude and longitude from degrees to radians
        double latitude1 = DegreesToRadians(point1[0]);
        double latitude2 = DegreesToRadians(point1[0]);
        double longitude1 = DegreesToRadians(point1[1]);
        double longitude2 = DegreesToRadians(point1[1]);

        // Calculate differences in latitudeitudes and longitudegitudes
        double deltaLatitude = latitude2 - latitude1;
        double deltaLongitude = longitude2 - longitude1;

        // Calculate half angles
        double sinDeltaLatitude = Math.Sin(deltaLatitude / 2);
        double sinDeltaLongitude = Math.Sin(deltaLongitude / 2);

        // Haversine formula
        double a = sinDeltaLatitude * sinDeltaLatitude;
        double b = Math.Cos(latitude1) * Math.Cos(latitude2) * sinDeltaLongitude * sinDeltaLongitude;

        // Calculate the distance
        return 2 * radius * Math.Asin(Math.Sqrt(a + b));
    }

    /// <summary>
    /// Calculates the Minkowski distance, which is a general form av distance measure that includes euclidean and Manhattan distance as special cases where the parameter p dictates the form of distance. 
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <param name="p">Dictates the form of distances measured</param>
    /// <returns>The Minkowski distance</returns>
    /// <exception cref="ArgumentException">Thrown if the dimensions of the points differ.</exception>
    public static double MinkowskiDistance(double[] point1, double[] point2, double p)
    {
        if (point1.Length != point2.Length)
        {
            throw new ArgumentException("Data points must have the same number of dimensions.");
        }

        // Formel: (sum(abs(x_i - y_i) ^ p)) ^ (1 / p)
        double sumOfPoweredDifferences = 0;

        for (int i = 0; i < point1.Length; i++)
        {
            double difference = point1[i] - point2[i];

            sumOfPoweredDifferences += Math.Abs(Math.Pow(difference, p));
        }

        return Math.Sqrt(sumOfPoweredDifferences);
    }

    private static double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

    public static double JaccardDistance(T[] point1, T[] point2)
    {
        IEnumerable<T> coordinates1 = (IEnumerable<T>)point1.GetEnumerator();
        IEnumerable<T> coordinates2 = (IEnumerable<T>)point2.GetEnumerator();

        if (coordinates1.Count() != coordinates2.Count())
        {
            throw new ArgumentException("Vectors must have the same number of dimensions.");
        }

        int intersection = coordinates1.Zip(coordinates2, (x, y) =>
            EqualityComparer<T>.Default.Equals(x, y) ? 1 : 0).Sum();
        int union = coordinates1.Union(coordinates2).Count();

        if (union == 0)
        {
            return 0;
        }

        return 1 - ((double)intersection / union);
    }
}

/*
TODO: Implement the following methods to calculate distances:
* Hamming distance: 
  Used first and formost to compare strings of equal length and to cound the number of positions where the strings differ.
  Formula: The number of positions that the strings have different characters. 

* Levenshtein distance (Also known as Edit Distance): 
  Measures the number of operations needed to convert one string to another, which is used in text analysis and stringmatching. 
  Formula: Uses a dynamic programming method to calculate the number of operations (inserts, removals or substitutions) required to transform one string into another. 

* Dynamic Time Warping (DTW): 
  Used to measure the similarity between two time series by finding the optimal match between the time steps. 
  Formula: Uses a dynamic programming method to find the optimal match between the time steps in two time series.  

* Euclidean Minimum Spanning Tree (EMST): 
  A method to use euclidean distance to create a tree that connects to all data points with a minimal total length. 
  Formula: Calculate the minimum tree that spans all data points with the minimum total length calculated by euclidean distance. 

*/

/*
TODO: Improve the code:

* Investigate the possibility to avoid dynamic type conversions to improve the efficiency of the algorithms. Is it possible to use generic types instead of dynamic type conversions?

* Complete the documentation with more details regarding parameters and return types for each method to clarify those. 

* General code structure: Review the code to see if there are possibilities to further section the code in smaller and more specialized methods to improve maintainability and reusability. 

* Error handling: Review the error handling of the code to see that it is sufficient to handle all potential errors that could arrise during execution. 
  Make sure that error messages that are thrown are informative and helps the use to understand what went wrong and how it can be adressed. 

* Implement unit tests to ensure that each method functions correctly and returns expected results. 

* Review the calculation methods to see if they can be optimized further to improve efficiency, such as using more efficient 
  algorithms or parallellizing the calulations. 

*/
