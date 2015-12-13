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
    public class ProductTableController : Controller
    {
        public ProductTableController()
        {
            Mapper.CreateMap<ProductModel, Product>();
            Mapper.CreateMap<Product, ProductModel>();
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
        public ActionResult GetProducts()
        {
            var products = new ProductRepository().Items.ToList();
            var res = Mapper.Map<IList<Product>, IList<ProductModel>>(products);
            return PartialView("ProductView", res);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddProduct()
        {
            var product = new ProductModel();
            return PartialView("AddProductView", product);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            List<string> _errList = new List<string>();
            bool _success = false;

            if (ModelState.IsValid)
            {
                try
                {
                    new ProductRepository().Add(Mapper.Map<Product>(product));
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
        public ActionResult EditProduct(int id)
        {
            var product = Mapper.Map<ProductModel>(new ProductRepository().ProductObjectById(id));
            return PartialView("EditProductView", product);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditProduct(ProductModel product)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new ProductRepository().Update(product.Id, Mapper.Map<Product>(product));
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
        public ActionResult DeleteProductR(int id)
        {
            var product = new ProductModel(){ Id = id };
            return PartialView("DeleteProductView", product);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    new ProductRepository().Remove(id);
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