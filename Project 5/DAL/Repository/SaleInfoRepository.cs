using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class SaleInfoRepository : AbstractRepository, IRepository<SaleInfo>, IEnumerable<SaleInfo>
    {
        internal static Model.SaleInfo ToEntity(SaleInfo saleInfo)
        {
            return new Model.SaleInfo()
            {
                Id = saleInfo.Id,
                ClientId = saleInfo.Client.Id,
                FileId = saleInfo.FileInformation.Id,
                ProductId = saleInfo.Product.Id,
                Date = saleInfo.Date,
                Cost = saleInfo.Cost
            };
        }
        internal static SaleInfo ToObject(Model.SaleInfo saleInfo)
        {
            return saleInfo == null ? null : new SaleInfo(saleInfo.Id, saleInfo.Date, FileInformationRepository.ToObject(saleInfo.File),
                ClientsRepository.ToObject(saleInfo.Client), ProductRepository.ToObject(saleInfo.Product), saleInfo.Cost);
        }

        private static Model.Client AddIfNotAndGetClient(string firstName, string secondName)
        {
            return new ClientsRepository().AddIfNotAndGetClient(firstName, secondName);
        }
        private static Model.Product AddIfNotAndGetProduct(string name)
        {
            return new ProductRepository().AddIfNotAndGetProduct(name);
        }
        private static Model.FileInformation AddIfNotAndGetFile(string fileName, DateTime date, string managerName)
        {
            return new FileInformationRepository().AddIfNotAndGetFile(fileName, date, managerName);
        }

        public void Add(SaleInfo item)
        {
            if (item == null)
                throw new DalException("SaleInfo can not be null");
            if (item.Client == null)
                throw new DalException("Client in saleInfo can not be null");
            if (item.Product == null)
                throw new DalException("Product in saleInfo can not be null");
            if (item.FileInformation == null)
                throw new DalException("FileInformation in saleInfo can not be null");

            Model.SaleInfo rr;
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var client = AddIfNotAndGetClient(item.Client.FirstName, item.Client.SecondName);
                    var product = AddIfNotAndGetProduct(item.Product.Name);
                    var file = AddIfNotAndGetFile(item.FileInformation.Name, item.FileInformation.Date, item.FileInformation.Manager.SecondName);
                    item.Client.Id = client.Id;
                    item.Product.Id = product.Id;
                    item.FileInformation.Id = file.Id;
                    rr = Context.SaleInfoSet.Add(ToEntity(item));
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    throw ex;
                }
            }
            Context.SaveChanges();
            item.Id = rr.Id;
        }
        public void Update(int id, SaleInfo item)
        {
            if (item == null)
                throw new DalException("SaleInfo can not be null");

            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    var element = SaleInfoById(id);
                    if (element == null)
                        throw new DalException("SaleInfo with this ID is not found");

                    if (item.Client == null)
                        throw new DalException("Client can not be null");
                    if (item.Product == null)
                        throw new DalException("Product can not be null");

                    var client = AddIfNotAndGetClient(item.Client.FirstName, item.Client.SecondName);
                    var product = AddIfNotAndGetProduct(item.Product.Name);

                    element.Date = item.Date;
                    element.ClientId = client.Id;
                    element.ProductId = product.Id;
                    element.Cost = item.Cost;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            Context.SaveChanges();
        }
        public void Remove(SaleInfo item)
        {
            if (item == null)
                throw new DalException("SaleInfo can not be null");

            var element = SaleInfoById(item.Id);
            if (element == null)
                throw new DalException("SaleInfo with this ID is not found");

            Context.SaleInfoSet.Remove(element);
            Context.SaveChanges();
        }
        public void Remove(int id)
        {
            var element = SaleInfoById(id);
            if (element == null)
                throw new DalException("SaleInfo with this ID is not found");

            Context.SaleInfoSet.Remove(element);
            Context.SaveChanges();
        }
        private Model.SaleInfo SaleInfoById(int id)
        {
            return Context.SaleInfoSet.FirstOrDefault(x => x.Id == id);
        }

        public SaleInfo SaleInfoObjectById(int id)
        {
            var saleInfo = Context.SaleInfoSet.FirstOrDefault(x => x.Id == id);
            return saleInfo == null ? null : ToObject(saleInfo);
        }

        

        public IEnumerable<SaleInfo> Items
        {
            get { return Context.SaleInfoSet.AsEnumerable().Select(item => SaleInfoObjectById(item.Id)); }
        }
        public IEnumerator<SaleInfo> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
