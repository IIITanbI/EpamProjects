using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(int id, T item);
        IEnumerable<T> Items { get; }
        
    }
}
