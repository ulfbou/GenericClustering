namespace GenericClustering.CoefficientCalculators;

/*
Hierarkisk klustring är en familj av algoritmer som organiserar datapunkter i ett hierarkiskt trädstruktur, där varje nod i trädet representerar ett kluster av datapunkter. Det finns två huvudsakliga metoder för hierarkisk klustring: agglomerativ och divisiv.

Agglomerativ Hierarkisk Klustring:

Steg 1: Initialisering: Börja med att behandla varje datapunkt som ett enskilt kluster.
Steg 2: Beräkning av avstånd: Beräkna avståndet mellan alla par av kluster. Avståndet kan beräknas med olika metoder, till exempel enklaste avstånd, minsta avstånd, största avstånd eller medelavstånd mellan punkter i klustren.
Steg 3: Sammanfoga kluster: Sammanfoga de två närmaste klustren baserat på det beräknade avståndet. Detta upprepas tills alla datapunkter är en del av ett enda kluster eller tills antalet kvarvarande kluster når en fördefinierad gräns.
Steg 4: Skapa hierarkisk struktur: Resultatet är en hierarkisk struktur där varje nod representerar ett kluster och varje kluster antingen består av en datapunkt eller har andra kluster som barnnoder. Hierarkin representeras ofta som ett dendrogram.


Divisiv Hierarkisk Klustring:
Steg 1: Initialisering: Börja med alla datapunkter som en del av ett enda kluster.
Steg 2: Beräkning av avstånd: Beräkna avståndet mellan alla par av datapunkter eller kluster.
Steg 3: Dela kluster: Dela upp det största klustret i två mindre kluster baserat på det beräknade avståndet. Detta upprepas tills varje kluster bara innehåller en datapunkt eller tills antalet kvarvarande kluster når en fördefinierad gräns.
Steg 4: Skapa hierarkisk struktur: Resultatet är också en hierarkisk struktur där varje nod representerar ett kluster och varje kluster antingen består av en datapunkt eller har andra kluster som barnnoder.
Ofta används metoder som enklare avståndsmått (t.ex. euklidiskt avstånd) eller mer sofistikerade metoder som Ward's metod för att mäta avståndet mellan kluster.

Hierarkisk klustring är attraktiv eftersom den ger en hierarkisk struktur som kan visualiseras som ett dendrogram, vilket kan vara användbart för att analysera relationer mellan kluster på olika nivåer av detaljer.
 */
internal class HierarchicalClustering<T> where T : struct, IComparable<T>
{
    private List<Cluster<T>> Clusters;

    public List<IDataPoint<T>> DataPoints { get; private set; }

    public HierarchicalClustering(List<IDataPoint<T>> dataPoints)
    {
        Clusters = new List<Cluster<T>>();
        DataPoints = dataPoints;
    }

    public void AgglomerativeCluster()
    {
        // Initialisera varje datapunkt som ett enskilt kluster
        foreach (var point in DataPoints)
        {
            var cluster = new Cluster<T>(point);
            Clusters.Add(cluster);
        }

        // Utför agglomerativ hierarkisk klustring
        while (Clusters.Count > 1)
        {
            // Här implementeras logiken för att sammanfoga närliggande kluster
            MergeClosestClusters();
        }
    }

    public void DivisiveCluster(int minClusters)
    {

        // Initialisera varje datapunkt som ett enskilt kluster
        Clusters.Add(new Cluster<T>(DataPoints));

        // Utför divisiv hierarkisk klustring
        while (Clusters.Count < minClusters)
        {
            // Här implementeras logiken för att dela upp det största klustret
            SplitLargestCluster();
        }
    }

    private void MergeClosestClusters()
    {
        // Metod för att sammanfoga de två närmaste klustren baserat på avståndet mellan dem
        // Implementeras genom att beräkna avståndet mellan varje par av kluster och sedan sammanfoga de närmaste
/*
Enklaste avstånd(Single Linkage):

Beräknar avståndet mellan två kluster som det minsta avståndet mellan någon datapunkt i det första klustret och någon datapunkt i det andra klustret.
Fördelar:
Känslig för långa smala kluster och brus.
Nackdelar:
Kan leda till "chaining effect", där kluster kedjas samman även om de är långt ifrån varandra.

Största avstånd(Complete Linkage):
Beräknar avståndet mellan två kluster som det största avståndet mellan någon datapunkt i det första klustret och någon datapunkt i det andra klustret.
Fördelar:
Mindre känslig för "chaining effect" jämfört med enklaste avståndet.
Nackdelar:
        Kan leda till kluster som är av ojämn storlek och kan missa subkluster.

        Medelavstånd(Average Linkage):
Beräknar avståndet mellan två kluster som genomsnittet av avstånden mellan alla datapunkter i det första klustret och alla datapunkter i det andra klustret.
Fördelar:
Mindre känslig för brus jämfört med enklaste avståndet.
Nackdelar:
        Kan leda till "fusing effect", där kluster fuseras även om de inte är nära varandra.

Centroidavstånd:
        Beräknar avståndet mellan två kluster som avståndet mellan deras centroider(genomsnittliga datapunkter).
Fördelar:
        Ger en balans mellan enklaste och största avståndet.
Nackdelar:
        Känslig för brus och icke - symmetriskt avstånd.
*/
}

    private void SplitLargestCluster()
    {
        // Metod för att dela upp det största klustret i två mindre kluster
        // Implementeras genom att välja det största klustret och dela det i två mindre kluster baserat på något kriterium
    }

    public List<Cluster<T>> GetClusters()
    {
        // Metod för att hämta de resulterande klustren
        return Clusters;
    }
}

