namespace GenericClustering.CoefficientCalculators;

/*
**DBSCAN (Density-Based Spatial Clustering of Applications with Noise)** is a powerful data clustering algorithm that operates based on the assumption that clusters are dense regions in space separated by areas of lower density. Let's delve into the basics of DBSCAN:

1. **Density-Based Clustering**:
   - DBSCAN groups data points that are **densely grouped** into a single cluster. It identifies clusters in large spatial datasets by examining the **local density** of the data points.
   - The algorithm doesn't require specifying the number of clusters beforehand, making it a **non-parametric** approach.

2. **Core Points, Reachable Points, and Outliers**:
   - To understand DBSCAN, consider a set of points in some space to be clustered.
   - Parameters:
     - **ε (eps)**: Specifies the radius of a neighborhood around each point.
     - **minPts**: Minimum number of points within distance ε to classify a point as a core point.
   - Classification:
     - **Core Point**: A point with at least minPts neighbors within distance ε.
     - **Directly Reachable**: A point q is directly reachable from a core point p if q is within distance ε from p.
     - **Reachable**: A point q is reachable from p if there exists a path from p to q where each intermediate point is directly reachable from the previous one.
     - **Outliers**: Points that are not reachable from any other point.

3. **Algorithm Steps**:
   - Find the points in the ε-neighborhood of every point and identify the **core points** (those with more than minPts neighbors).
   - Construct a neighbor graph based on the core points.
   - Find the connected components of core points, ignoring non-core points.

DBSCAN is widely used in practice and has received substantial attention in both theory and application. Its flexibility and ability to handle noise make it a valuable tool for various clustering tasks. If you're working with spatial data, give DBSCAN a try! 🌟

For more details, you can refer to the [Wikipedia page on DBSCAN](https://en.wikipedia.org/wiki/DBSCAN) ¹ or explore its implementation using libraries like [scikit-learn](https://scikit-learn.org/stable/auto_examples/cluster/plot_dbscan.html) ⁴.

Source: Conversation with Bing, 3/18/2024
(1) DBSCAN - Wikipedia. https://en.wikipedia.org/wiki/DBSCAN.
(2) Demo of DBSCAN clustering algorithm — scikit-learn 1.4.1 documentation. https://scikit-learn.org/stable/auto_examples/cluster/plot_dbscan.html.
(3) How Does DBSCAN Clustering Work? Understanding the Basics. https://www.analyticsvidhya.com/blog/2020/09/how-dbscan-clustering-works/.
(4) DBSCAN - Wikipedia - BME. https://static.hlt.bme.hu/semantics/external/pages/deep_learning/en.wikipedia.org/wiki/DBSCAN.html.
(5) DBSCAN Clustering in ML | Density based clustering. https://www.geeksforgeeks.org/dbscan-clustering-in-ml-density-based-clustering/.
 */
internal class DBSCAN<T> where T : struct, IComparable<T>
{
    public DBSCAN(List<IDataPoint<T>> dataPoints, double epsilon, int minPoints) { }
    public void Cluster() { }
    public List<Cluster<T>> GetClusters()=> null;
}
