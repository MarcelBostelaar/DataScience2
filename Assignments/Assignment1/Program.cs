using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {

        const int K = 4;
        const int N = 100;
        const int I = 500;

        static void Main(string[] args)
        {
            Func<float[], float[], float> distance = (x, y) =>
            {
                return (float)Math.Sqrt(x.Select((z, i) => Math.Pow(z - y[i], 2)).Aggregate((a, b) => a + b));
            };
            Func<float[], float[], float[]> avarageOfTwo = (x, y) =>
            {
                return x.Select((a, i) => (a + y[i]) / 2).ToArray();
            };
            Func<IEnumerable<float[]>, float[]> avarageMany = x => x.Aggregate(new float[Data.data[0].Length], avarageOfTwo);
            var equality = new GenericEquality<float[]>((a, b) => a.SequenceEqual(b), x => x.GetHashCode());

            double error = double.PositiveInfinity;
            IEnumerable<Cluster<float[]>> bestcluster = null;

            for (int j = 0; j < I; j++)
            {
                var clusters = KMeans.Kmeans<float[]>(N, K, Data.data, distance, avarageMany, equality);

                double total_error = 0;
                foreach (var i in clusters)
                {
                    foreach (var x in i.Points)
                    {
                        total_error += Math.Pow(distance(i.Centroid, x), 2);
                    }
                }
                if(total_error < error)
                {
                    error = total_error;
                    bestcluster = clusters;
                }
                Console.WriteLine(error);
                Console.WriteLine(total_error);
            }
            Console.ReadKey();
        }
    }
}
