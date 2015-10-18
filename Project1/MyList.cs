using System;
using System.Collections.Generic;

namespace Project1
{
    public interface IMyCollection<T> : ICollection<T>, ICloneable
    {

    }

    class MyList<T> :List<T>, IMyCollection<T>
    {
        public object Clone()
        {
            var copy = new MyList<T>();
            copy.AddRange(this);
            return copy;
        }
    }
}