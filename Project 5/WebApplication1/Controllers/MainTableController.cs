using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "user")]
    public class MainTableController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var selectList = new List<SelectListItem>();
            var tt = Enum.GetNames(typeof (TableEnum));
            for (int i = 0; i < tt.Length; i++)
            {
                if (tt[i] == TableEnum.Users.ToString())
                    if (!(User.Identity.IsAuthenticated && User.IsInRole("admin")))
                        continue;
   
                selectList.Add(new SelectListItem()
                {
                    Text = tt[i],
                    Value = tt[i],
                    Selected = i==0
                });
            }

            return View("Index", selectList);
        }
        public ActionResult GetTable(string tableName)
        {
            TableEnum tt = (TableEnum)Enum.Parse(typeof(TableEnum), tableName);
            switch (tt)
            {
                case TableEnum.None:
                    return new EmptyResult();
                case TableEnum.Clients:
                    return RedirectToAction("GetClients", "ClientTable");
                case TableEnum.Managers:
                    return RedirectToAction("GetManagers", "ManagerTable");
                case TableEnum.Products:
                    return RedirectToAction("GetProducts", "ProductTable");
                case TableEnum.FileInformations:
                    return RedirectToAction("GetFileInformations", "FileInformationTable");
                case TableEnum.SaleInfos:
                    return RedirectToAction("GetSaleInfos", "SaleInfoTable");
                case TableEnum.Users:
                    return RedirectToAction("GetUsers", "UserTable");
                default:
                    return View("Error");
            }
        }

    }
}
