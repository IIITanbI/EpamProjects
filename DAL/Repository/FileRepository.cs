using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class FileRepository : AbstractRepository, IRepository<File>, IEnumerable<File>
    {
        internal static Model.File ToEntity(File file)
        {
            return new Model.File()
            {
                Id = file.Id,
                Name = file.Name,
                Date = file.Date,
                ManagerId = file.Manager.Id
            };
        }
        internal static File ToObject(Model.File file)
        {
            return file == null ? null : new File(file.Id, file.Name, file.Date, ManagerRepository.ToObject(file.Manager));
        }

        private Model.Manager ManagerByName(string secondName)
        {
            return new ManagerRepository().ManagerByName(secondName);
        }
        private Model.Manager AddIfNotAndGetManager(string secondName)
        {
            return new ManagerRepository().AddIfNotAndGetManager(secondName);
        }
        private Model.File FileInfoById(int id)
        {
            return Context.FileSet.FirstOrDefault(x => x.Id == id);
        }

        public void Add(File item)
        {
            if (item == null)
                throw new ArgumentException("File can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var manager = AddIfNotAndGetManager(item.Manager.SecondName);
                    item.Manager.Id = manager.Id;
                    Context.FileSet.Add(ToEntity(item));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
        }
        public void Update(int id, File item)
        {
            if (item == null)
                throw new ArgumentException("File can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = FileInfoById(id);
                    var manager = AddIfNotAndGetManager(item.Manager.SecondName);
                    if (element == null)
                        throw new ArgumentException("File with this ID is not found");
                    if (manager == null)
                        throw new ArgumentException("Manager with this name is not found");

                    element.Name = item.Name;
                    element.Date = item.Date;
                    element.ManagerId = manager.Id;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
        public void Remove(File item)
        {
            if (item == null)
                throw new ArgumentException("File can not be null");
            var element = FileInfoById(item.Id);
            if (element == null)
                throw new ArgumentException("File with this ID is not found");
            Context.FileSet.Remove(element);
            Context.SaveChanges();
        }


        public IEnumerable<File> Items
        {
            get { return Context.FileSet.AsEnumerable().Select(file => ToObject(file)); }
        }
        public IEnumerator<File> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
