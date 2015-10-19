using System.Collections.Generic;

namespace Project1
{
    class CloneList<T> : List<T>, ICloneCollection<T>
    {
        public object Clone()
        {
            var copy = new CloneList<T>();
            copy.AddRange(this);
            return copy;
        }
    }
}