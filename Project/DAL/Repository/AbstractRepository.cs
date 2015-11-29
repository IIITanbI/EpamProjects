using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Repository
{
    public class AbstractRepository : IDisposable
    {
        private bool disposed;
        protected ModelContainer Context { get; }

        protected AbstractRepository()
        {
            Context = new ModelContainer();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
   
            if (disposing)
            {
                Context.Dispose();
            }
            
            disposed = true;
        }

        ~AbstractRepository()
        {
            Dispose(false);
        }
    }
}
