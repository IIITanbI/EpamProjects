using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class ClientsRepository : AbstractRepository, IRepository<Client>, IEnumerable<Client>
    {
        internal static Model.Client ToEntity(Client client)
        {
            return new Model.Client()
            {
                 FirstName = client.FirstName,
                 SecondName = client.SecondName
            };
        }
        internal static Client ToObject(Model.Client client)
        {
            return client == null ? null : new Client(client.Id, client.FirstName, client.SecondName);
        }

        private static void Check(Client item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }
        private static void Check(Model.Client item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
        }

        public void Add(Client item)
        {
            if (item == null)
                throw new ArgumentException("Client can not be null");

            Context.ClientSet.Add(ToEntity(item));
            Context.SaveChanges();
        }
        public void Update(int id, Client item)
        {
            if (item == null)
                throw new ArgumentException("Client can not be null");
            //Check(item);
            var element = ClientById(item.Id);
            if (element == null)
                throw new ArgumentException("Client with this ID is not found");
            //Check(element);

            element.FirstName = item.FirstName;
            element.SecondName = item.SecondName;
            Context.SaveChanges();
        }
        public void Remove(Client item)
        {
            if (item == null)
                throw new ArgumentException("Client can not be null");
            //Check(item);
            var element = ClientById(item.Id);
            if (element == null)
                throw new ArgumentException("Client with this ID is not found");
            // Check(element);

            Context.ClientSet.Remove(element);
            Context.SaveChanges();
        }

        
        private Model.Client ClientById(int id)
        {
            return Context.ClientSet.FirstOrDefault(x => x.Id == id);
        }
        public Client ClientModelById(int id)
        {
            var client = ClientById(id);
            return ToObject(client);
        }
        internal Model.Client ClientByName(string firstName, string secondName)
        {
            return Context.ClientSet.FirstOrDefault(x => x.FirstName == firstName && x.SecondName == secondName);
        }
        internal Model.Client AddIfNotAndGetClient(string firstName, string secondName)
        {
            var client = ClientByName(firstName, secondName);
            if (client == null)
            {
                client = Context.ClientSet.Add(ToEntity(new Client(firstName, secondName)));
                Context.SaveChanges();
            }
            return client;
        }


        public IEnumerable<Client> Items
        {
            get { return Context.ClientSet.AsEnumerable().Select(item => ToObject(item)); }
        }
        public IEnumerator<Client> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
