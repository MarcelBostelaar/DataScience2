using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    static class KMeans
    {
        public static IEnumerable<Cluster<T>> Kmeans<T>(int iterations, int K, IEnumerable<T> points, Func<T, T, float> distance, Func<IEnumerable<T>, T> average, IEqualityComparer<T> equality)
        {
            Cluster<T>[] initialCentroids = new Cluster<T>[K];
            Random RNG = new Random();
            int numberofpoints = points.Count();
            if(K > numberofpoints)
            {
                throw new Exception("K cannot be larger than # of points");
            }
            for (int i = 0; i < K; i++)
            {
                initialCentroids[i] = new Cluster<T>(points.ElementAt(RNG.Next(0, numberofpoints)));
            }
            if (initialCentroids.GroupBy(x => x.Centroid, equality).Count() != K)
            {
                return Kmeans(iterations, K, points, distance, average, equality);
            }
            return Kmeans(iterations, initialCentroids, points, distance, average, equality);
        }

        public static IEnumerable<Cluster<T>> Kmeans<T>(int iterations, IEnumerable<Cluster<T>> K, IEnumerable<T> points, Func<T, T, float> distance, Func<IEnumerable<T>, T> average, IEqualityComparer<T> equality)
        {
            while(iterations > 0)
            {
                iterations--;
                foreach (var point in points)
                {
                    K.OrderBy(x => distance(x.Centroid, point)).First().Points.Add(point);
                }
                var NewK = K.Select(x => new Cluster<T>(average(x.Points))).ToArray();
                if (K.Select(x => x.Centroid).Except(NewK.Select(x => x.Centroid), equality).Count() <= 0)
                {
                    iterations = 0;
                }
                K = NewK;
            }
            foreach (var point in points)
            {
                K.OrderBy(x => distance(x.Centroid, point)).First().Points.Add(point);
            }
            return K;
        }
    }
}
