using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class FileInformationRepository : AbstractRepository, IRepository<FileInformation>, IEnumerable<FileInformation>
    {
        internal static Model.FileInformation ToEntity(FileInformation fileInformation)
        {
            return new Model.FileInformation()
            {
                Id = fileInformation.Id,
                Name = fileInformation.Name,
                Date = fileInformation.Date,
                ManagerId = fileInformation.Manager.Id
            };
        }
        internal static FileInformation ToObject(Model.FileInformation file)
        {
            return file == null ? null : new FileInformation(file.Id, file.Name, file.Date, ManagerRepository.ToObject(file.Manager));
        }

        internal Model.Manager ManagerByName(string secondName)
        {
            return new ManagerRepository().ManagerByName(secondName);
        }
        internal Model.Manager AddIfNotAndGetManager(string secondName)
        {
            return new ManagerRepository().AddIfNotAndGetManager(secondName);
        }
        internal Model.FileInformation FileById(int id)
        {
            return Context.FileInformationSet.FirstOrDefault(x => x.Id == id);
        }

        public void Add(FileInformation item)
        {
            if (item == null)
                throw new ArgumentException("File can not be null");
            var fileExtension = ConfigurationManager.ConnectionStrings["ModelContainer"];
            

            Console.WriteLine(fileExtension);
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var manager = AddIfNotAndGetManager(item.Manager.SecondName);
                    item.Manager.Id = manager.Id;
                    Context.FileInformationSet.Add(ToEntity(item));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }
            }
            foreach (var mm in Context.ManagerSet)
            {
                Console.WriteLine(mm.Id + " " + mm.SecondName);
            }
            Console.WriteLine(Context.FileInformationSet.Local.Count);

        }
        public void Update(int id, FileInformation item)
        {
            if (item == null)
                throw new ArgumentException("File can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = FileById(id);
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
        public void Remove(FileInformation item)
        {
            if (item == null)
                throw new ArgumentException("File can not be null");
            var element = FileById(item.Id);
            if (element == null)
                throw new ArgumentException("File with this ID is not found");
            Context.FileInformationSet.Remove(element);
            Context.SaveChanges();
        }


        public IEnumerable<FileInformation> Items
        {
            get { return Context.FileInformationSet.AsEnumerable().Select(file => ToObject(file)); }
        }
        public IEnumerator<FileInformation> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
