using System.Collections.Generic;

namespace Project1
{
    class MyList<T> : List<T>, IMyCollection<T>
    {
        public object Clone()
        {
            var copy = new MyList<T>();
            copy.AddRange(this);
            return copy;
        }
    }
}