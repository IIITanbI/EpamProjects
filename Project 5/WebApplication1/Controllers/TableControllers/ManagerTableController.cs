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
    public class ManagerTableController : Controller
    {
        public ManagerTableController()
        {
            Mapper.CreateMap<ManagerModel, Manager>();
            Mapper.CreateMap<Manager, ManagerModel>();
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
        public ActionResult GetManagers()
        {
            var managers = new ManagerRepository().Items.ToList();
            var res = Mapper.Map<IList<Manager>, IList<ManagerModel>>(managers);
            return PartialView("ManagerView", res);
        }

        [HttpGet]
        public ActionResult AddManager()
        {
            var manager = new ManagerModel();
            return PartialView("AddManagerView", manager);
        }
        [HttpPost]
        public ActionResult AddManager(ManagerModel manager)
        {
            List<string> _errList = new List<string>();
            bool _success = false;

            if (ModelState.IsValid)
            {
                try
                {
                    new ManagerRepository().Add(Mapper.Map<Manager>(manager));
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

        [HttpGet]
        public ActionResult EditManager(int id)
        {
            var manager = Mapper.Map<ManagerModel>(new ManagerRepository().ManagerObjectById(id));
            return PartialView("EditManagerView", manager);
        }
        [HttpPost]
        public ActionResult EditManager(ManagerModel manager)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new ManagerRepository().Update(manager.Id, Mapper.Map<Manager>(manager));
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


        [HttpGet]
        public ActionResult DeleteManagerR(int id)
        {
            var manager = new ManagerModel() { Id = id };
            return PartialView("DeleteManagerView", manager);
        }
        [HttpPost]
        public ActionResult DeleteManager(int id)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new ManagerRepository().Remove(id);
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