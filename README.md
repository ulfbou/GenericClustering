### GenericClustering

GenericClustering is an open source project that offers a set of tools for Cluster analysis and data mining implemented in C# .NET. The project is designed to be flexible and scalable to handle various datatypes and analytical needs. 

### Overview

What is cluster analysis?

Cluster analysis is a technology within computational science for grouping similar objects based on their properties or behavior. It is an important method to find patterns and structures in data sets, which can be useful to make predictions, understand user behavior, or identify deviations in patterns.

What can GenericClustering do?

K-means cluster analysis
Hierarchical cluster analysis
Density-Based Spatial Clustering of Applications with Noise (DBSCAN)
Preprocessing data
Distance measurements
Visualisation of cluster results

### Installation
Clone the project to your local machine:
bash
Copy code
git clone https://github.com/ulfbou/GenericClustering.git
Open the project in Visual Studio or your chosen IDE.

### Usage

Sample code

// Create a data set.
List<IDataPoint<double>> dataPoints = new List<DataPoint<double>>();
dataPoints.Add(new DataPoint<double>(1.0, 2.0));
dataPoints.Add(new DataPoint<double>(3.0, 4.0));
dataPoints.Add(new DataPoint<double>(5.0, 6.0));

// Create an instance of KMeans. 
int k = 2;

// Initialize cluster
KMeans<double> kmeans = new KMeans<double>(dataPoints, k);

// Perform the cluster analysis
kmeans.Run();

// Fetch the resultat
IEnumerable<Cluster<double>> clusters = kmeans.GetAllClusters();

### Important classes
DataPoint<T>: Represents a data point in a multidimensional space. 
KMeans<T>: Implements K-means cluster analysis algorithm. 
HierarchicalClustering<T>: Provides support for hierarchical cluster analysis.
DBSCAN<T>: Performs Density-Based Spatial Clustering of Applications with Noise.

### TODO

Certain parts of the project are not yet implemented and are marked as TODO. This includes:

- Hierarchical cluster analysis
- Density-Based Spatial Clustering of Applications with Noise (DBSCAN)
- Preprocessing data
- Visualisation of cluster results

We welcome contributions and suggestions to implement these functions and to improve the project further. 

### Contributions
We welcome contributions from developers and users! If you would like to contribute to the project, please open a pull-request with your changes or report possible bugs and suggestions of improvement by creating an issue. 

### License
GenericClustering is licensed under the MIT license. Se LICENSE for more information.
