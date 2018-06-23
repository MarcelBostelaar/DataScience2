using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    struct Cluster<T>
    {
        public Cluster(T Centroid)
        {
            this.Centroid = Centroid;
            Points = new List<T>();
        }
        public T Centroid;
        public List<T> Points;
    }
}
