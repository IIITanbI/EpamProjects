using System.Collections.Generic;
using Project1.Vegetables;

namespace Project1
{
    class CloneList<T> : List<T>, ICloneCollection<T>
    { 
        public CloneList()
        {
    
        }
        public CloneList(IEnumerable<T> lst)
            :base(lst)
        {
            
        }

        public object Clone()
        {
            var copy = new CloneList<T>();
            copy.AddRange(this);
            return copy;
        }
    }
    
}