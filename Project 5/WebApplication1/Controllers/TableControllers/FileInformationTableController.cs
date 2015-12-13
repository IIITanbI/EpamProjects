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
    public class FileInformationTableController : Controller
    {
        public FileInformationTableController()
        {
            Mapper.CreateMap<FileInformationModel, FileInformation>().ForMember(dest => dest.Manager, opts => opts.MapFrom(src => new Manager(src.ManagerName)));
            Mapper.CreateMap<FileInformation, FileInformationModel>().ForMember(dest => dest.ManagerName, opts => opts.MapFrom(src => src.Manager.SecondName));
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
        public ActionResult GetFileInformations()
        {
            var fileInformations = new FileInformationRepository().Items.ToList();
            var res = Mapper.Map<IList<FileInformation>, IList<FileInformationModel>>(fileInformations);
            return PartialView("FileInformationView", res);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddFileInformation()
        {
            var fileInformation = new FileInformationModel();
            return PartialView("AddFileInformationView", fileInformation);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddFileInformation(FileInformationModel fileInformation)
        {
            List<string> _errList = new List<string>();
            bool _success = false;

            if (ModelState.IsValid)
            {
                try
                {
                    var ff = Mapper.Map<FileInformation>(fileInformation);
                    new FileInformationRepository().Add(Mapper.Map<FileInformation>(fileInformation));
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
        [Authorize(Roles = "admin")]
        public ActionResult EditFileInformation(int id)
        {
            var _fileInformation = new FileInformationRepository().FileInformationObjectById(id);
            if (_fileInformation == null)
                return View("Error");

            var fileInformation = Mapper.Map<FileInformationModel>(_fileInformation);
            return PartialView("EditFileInformationView", fileInformation);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditFileInformation(FileInformationModel fileInformation)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new FileInformationRepository().Update(fileInformation.Id, Mapper.Map<FileInformation>(fileInformation));
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
        [Authorize(Roles = "admin")]
        public ActionResult DeleteFileInformationR(int id)
        {
            var fileInformation = new FileInformationModel() { Id = id };
            return PartialView("DeleteFileInformationView", fileInformation);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteFileInformation(int id)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new FileInformationRepository().Remove(id);
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