﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repository
{
    public class ManagerRepository : AbstractRepository, IRepository<Manager>, IEnumerable<Manager>
    {
        internal static Model.Manager ToEntity(Manager manager)
        {
            return new Model.Manager()
            {
                Id = manager.Id,
                SecondName = manager.SecondName
            };
        }
        internal static Manager ToObject(Model.Manager manager)
        {
            return manager == null ? null : new Manager(manager.Id, manager.SecondName);
        }
       

        public void Add(Manager item)
        {
            if (item == null)
                throw new DalException("Manager can not be null");

            if (ManagerByName(item.SecondName) != null)
                throw new DalException("This manager already exist");

            var rr = Context.ManagerSet.Add(ToEntity(item));
            Context.SaveChanges();
            item.Id = rr.Id;
        }
        public void Update(int id, Manager item)
        {
            if (item == null)
                throw new DalException("Manager can not be null");
            var element = ManagerById(item.Id);
            if (element == null)
                throw new DalException("Manager with this ID is not found");

            element.SecondName = item.SecondName;
            Context.SaveChanges();
        }
        public void Remove(Manager item)
        {
            if (item == null)
                throw new DalException("Manager can not be null");
            var element = ManagerById(item.Id);
            if (element == null)
                throw new DalException("Manager with this ID is not found");

            Context.ManagerSet.Remove(element);
            Context.SaveChanges();
        }
        public void Remove(int id)
        {
            var element = ManagerById(id);
            if (element == null)
                throw new DalException("Manager with this ID is not found");

            Context.ManagerSet.Remove(element);
            Context.SaveChanges();
        }
        internal Model.Manager ManagerById(int id)
        {
            return Context.ManagerSet.FirstOrDefault(x => x.Id == id);
        }
        public Manager ManagerObjectById(int id)
        {
            var manager = Context.ManagerSet.FirstOrDefault(x => x.Id == id);
            return ToObject(manager);
        }
        internal Model.Manager ManagerByName(string secondName)
        {
            return Context.ManagerSet.FirstOrDefault(x => x.SecondName == secondName);
        }
        internal Model.Manager AddIfNotAndGetManager(string secondName)
        {
            var manager = ManagerByName(secondName);
            if (manager == null)
            {
                manager = Context.ManagerSet.Add(ToEntity(new Manager(secondName)));
                Context.SaveChanges();
            }
            return manager;
        }

        public IEnumerable<Manager> Items
        {
            get { return Context.ManagerSet.AsEnumerable().Select(item => ToObject(item)); }
        }
        public IEnumerator<Manager> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
