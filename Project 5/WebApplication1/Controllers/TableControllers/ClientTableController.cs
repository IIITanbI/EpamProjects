using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DAL.Models;
using DAL.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers.TableControllers
{
    [Authorize(Roles = "user")]
    public class ClientTableController : Controller
    {
        public ClientTableController()
        {
            Mapper.CreateMap<ClientModel, Client>();
            Mapper.CreateMap<Client, ClientModel>();
        }
        public List<string> GetModelErrors()
        {
            List<string> _errList = new List<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _errList.Add(error.ErrorMessage);
                }
            }
            return _errList;
        }

        
        [HttpGet]
        public ActionResult GetClients()
        {
            var clients = new ClientsRepository().Items.ToList();

            var res = Mapper.Map<IList<Client>, IList<ClientModel>>(clients);
            return PartialView("ClientView", res);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddClient()
        {
            ClientModel client = new ClientModel();
            return PartialView("AddClientView", client);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddClient(ClientModel client)
        {
            List<string> _errList = new List<string>();
            bool _success = false;

            if (ModelState.IsValid)
            {
                try
                {
                    new ClientsRepository().Add(Mapper.Map<Client>(client));
                    _success = true;
                }
                catch (DalException e)
                {
                    _errList.Add(e.Message);
                }
                catch (Exception)
                {
                    _errList.Add("DataBase Error");
                }
            }
            else
            {
                _errList = GetModelErrors();
            }

            return Json(new
            {
                success = _success,
                error = _errList
            });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditClient(int id)
        {
            var _client = new ClientsRepository().ClientModelById(id);
            if (_client == null)
                return View("Error");
            var client = Mapper.Map<ClientModel>(_client);
            return PartialView("EditClientView", client);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditClient(ClientModel client)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new ClientsRepository().Update(client.Id, Mapper.Map<Client>(client));
                    _success = true;
                }
                catch (DalException e)
                {
                    _errList.Add(e.Message);
                }
                catch (Exception)
                {
                    _errList.Add("DataBase Error");
                }
            }
            else
            {
                _errList = GetModelErrors();
            }

            return Json(new
            {
                success = _success,
                error = _errList
            });
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteClientR(int id)
        {
            var client = new ClientModel() {Id=id};
            return PartialView("DeleteClientView", client);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new ClientsRepository().Remove(id);
                    _success = true;
                }
                catch (DalException e)
                {
                    _errList.Add(e.Message);
                }
                catch (Exception)
                {
                    _errList.Add("DataBase Error");
                }
            }
            else
            {
                _errList = GetModelErrors();
            }

            return Json(new
            {
                success = _success,
                error = _errList
            });
        }
    }
}