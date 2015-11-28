using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class ProductRepository : AbstractRepository, IRepository<Product>, IEnumerable<Product>
    {
        internal static Model.Product ToEntity(Product product)
        {
            return new Model.Product()
            {
                Id = product.Id,
                Name = product.Name
            };
        }
        internal static Product ToObject(Model.Product product)
        {
            return product == null ? null : new Product(product.Id, product.Name);
        }

        private static void Check(Product item)
        {
            if (item == null)
                throw new ArgumentException("Product can not be null");
        }
        private static void Check(Model.Product item)
        {
            if (item == null)
                throw new ArgumentException("Product can not be null");
        }

        public void Add(Product item)
        {
            if (item == null)
                throw new ArgumentException("Product can not be null");
            //Check(item);
            Context.ProductSet.Add(ToEntity(item));
            Context.SaveChanges();
        }
        public void Update(int id, Product item)
        {
            if (item == null)
                throw new ArgumentException("Product can not be null");

            var element = ProductById(id);
            if (element == null)
                throw new ArgumentException("Product with this ID is not found");

            //Check(element);
            //Check(item);
            element.Name = item.Name;
            Context.SaveChanges();
        }
        public void Remove(Product item)
        {
            if (item == null)
                throw new ArgumentException("Product can not be null");
            //Check(item);
            var element = ProductById(item.Id);
            //Check(element);
            if (element == null)
                throw new ArgumentException("Product with this ID is not found");

            Context.ProductSet.Remove(element);
            Context.SaveChanges();
        }

        internal Model.Product ProductById(int id)
        {
            return Context.ProductSet.FirstOrDefault(x => x.Id == id);
        }
        internal Model.Product ProductByName(string name)
        {
            return Context.ProductSet.FirstOrDefault(x => x.Name == name);
        }
        public Product ProductObjectById(int id)
        {
            var product = Context.ProductSet.FirstOrDefault(x => x.Id == id);
            return ToObject(product);
        }
        internal Model.Product AddIfNotAndGetProduct(string name)
        {
            return ProductByName(name) ?? Context.ProductSet.Add(ToEntity(new Product(name)));
        }

        public IEnumerable<Product> Items
        {
            get { return Context.ProductSet.AsEnumerable().Select(item => ToObject(item)); }
        }
        public IEnumerator<Product> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
