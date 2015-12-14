using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
    public class SaleInfoTableController : Controller
    {
        public SaleInfoTableController()
        {
            Mapper.CreateMap<SaleInfo, SaleInfoModel>()
                .ForMember(dest => dest.ClientFirstName, opts => opts.MapFrom(src => src.Client.FirstName))
                .ForMember(dest => dest.ClientSecondName, opts => opts.MapFrom(src => src.Client.SecondName))
                .ForMember(dest => dest.ProductName, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.FileName, opts => opts.MapFrom(src => src.FileInformation.Name))
                .ForMember(dest => dest.ManagerName, opts => opts.MapFrom(src => src.FileInformation.Manager.SecondName));
            Mapper.CreateMap<SaleInfoModel, SaleInfo>();
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
        public ActionResult GetGrafics()
        {
            return PartialView("Graphics");
        }

        [HttpGet]
        public ActionResult GetManagerGraphic()
        {
            var srep = new SaleInfoRepository();
            var mrep = new ManagerRepository();

            var saleInfos = srep.Items.ToList();
            var managers = mrep.Items.ToList();
            var dict = managers.ToDictionary(manager => manager.Id, manager => 0);


            int total = saleInfos.Count;
            foreach (var saleInfo in saleInfos)
            {
                var _manager = saleInfo.FileInformation.Manager;
                try
                {
                    dict[_manager.Id]++;
                }
                catch (Exception e)
                { }
            }


            var res = new List<KeyValuePair<string, int>>();

            foreach (var entry in dict)
            {
                var manag = managers.FirstOrDefault(manager => manager.Id == entry.Key);
                res.Add(new KeyValuePair<string, int>(manag.SecondName, entry.Value));
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSaleInfos()
        {
            var saleInfos = new SaleInfoRepository().Items.ToList();
            var res = Mapper.Map<IList<SaleInfo>, IList<SaleInfoModel>>(saleInfos);
            return PartialView("SaleInfoView", res);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddSaleInfo()
        {
            var saleInfo = new SaleInfoModel();
            return PartialView("AddSaleInfoView", saleInfo);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddSaleInfo(SaleInfoModel saleInfo)
        {
            List<string> _errList = new List<string>();
            bool _success = false;

            if (ModelState.IsValid)
            {
                try
                {

                    //var tt = Mapper.Map<SaleInfo>(saleInfo);
                    var sin = new SaleInfo(saleInfo.Date, 
                        new FileInformation(saleInfo.FileName, saleInfo.Date, new Manager(saleInfo.ManagerName)),
                        new Client(saleInfo.ClientFirstName, saleInfo.ClientSecondName),
                        new Product(saleInfo.ProductName), saleInfo.Cost);

                    new SaleInfoRepository().Add(sin);
                    _success = true;
                }
                catch (DalException e)
                {
                    _errList.Add(e.Message);
                }
                catch (Exception x)
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
        public ActionResult EditSaleInfo(int id)
        {
            var _saleInfo = new SaleInfoRepository().SaleInfoObjectById(id);
            if (_saleInfo == null)
                return View("Error");

            var saleInfo = Mapper.Map<SaleInfoModel>(_saleInfo);
            return PartialView("EditSaleInfoView", saleInfo);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditSaleInfo(SaleInfoModel saleInfo)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var sin = new SaleInfo(saleInfo.Date,
                        new FileInformation(saleInfo.FileName, saleInfo.Date,
                        new Manager(saleInfo.ManagerName)),
                        new Client(saleInfo.ClientFirstName, saleInfo.ClientSecondName),
                        new Product(saleInfo.ProductName), saleInfo.Cost);

                    new SaleInfoRepository().Update(saleInfo.Id, sin);
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
        public ActionResult DeleteSaleInfoR(int id)
        {
            var saleInfo = new SaleInfoModel() { Id = id };
            return PartialView("DeleteSaleInfoView", saleInfo);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteSaleInfo(int id)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new SaleInfoRepository().Remove(id);
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