using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class GenericEquality<T> : IEqualityComparer<T>
    {
        Func<T, T, bool> _equals;
        Func<T, int> _hashcode;
        public GenericEquality(Func<T,T,bool> equals, Func<T, int> hashcode)
        {
            _equals = equals;
            _hashcode = hashcode;
        }
        public bool Equals(T x, T y)
        {
            return _equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return _hashcode(obj);
        }
    }
}
