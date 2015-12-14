using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using WebApplication1.Models;

namespace WebApplication1.Controllers.TableControllers
{
    [Authorize(Roles = "admin")]
    public class UserTableController : Controller
    {

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

        public UserTableController()
        {
            //Mapper.CreateMap<UserModel, ApplicationUser>()
            //    .ForMember(dest=>dest.UserName, opts=>opts.MapFrom(src=>src.Email))
            //    .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            //    .ForMember(dest => dest., opts => opts.MapFrom(src => src.Email))
        }
        [HttpGet]
        public ActionResult GetUsers()
        {
            List<ApplicationUser> users;
            using (var usersContext = new ApplicationDbContext())
            {
                users = usersContext.Users.ToList();
            }
            var res = users.Select(user => new UserModel { Email = user.Email, Password = user.PasswordHash });
            return PartialView("UserView", res);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            UserModel user = new UserModel();
            return PartialView("AddUserView", user);
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            List<string> _errList = new List<string>();
            bool _success = false;

            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var newUser = new ApplicationUser { Email = user.Email, UserName = user.Email };
                string password = user.Password;
                var result = userManager.Create(newUser, password);


                if (!result.Succeeded)
                {
                    _errList.AddRange(result.Errors);
                }
                else
                {
                    var res = userManager.AddToRole(newUser.Id, "user");
                    if (!res.Succeeded)
                        _errList.AddRange(res.Errors);
                    else _success = true;
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
        public ActionResult EditUser(string email)
        {
            ApplicationUser user;
            using (var usersContext = new ApplicationDbContext())
            {
                user = usersContext.Users.FirstOrDefault(apUser => apUser.Email == email);
            }
            if (user == null)
                return View("Error");

            UserModel res = new UserModel() { Email = user.Email, Password = user.PasswordHash };
            return PartialView("EditUserView", res);
        }
        [HttpPost]
        public ActionResult EditUser(UserModel user)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                    var newUser = new ApplicationUser { Email = user.Email, UserName = user.Email };
                    var result = userManager.UpdateAsync(newUser).Result;
                    if (!result.Succeeded)
                        _errList.AddRange(result.Errors);
                    else
                        _success = true;
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
        public ActionResult DeleteUserR(string email)
        {
            var user = new UserModel() { Email = email };
            return PartialView("DeleteUserView", user);
        }
        [HttpPost]
        public ActionResult DeleteUser(string email)
        {
            List<string> _errList = new List<string>();
            bool _success = false;
            if (ModelState.IsValid)
            {
                try
                {
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                    ApplicationUser user;
                    using (var usersContext = new ApplicationDbContext())
                    {
                        user = usersContext.Users.FirstOrDefault(apUser => apUser.Email == email);
                    }
                    if (user == null)
                        return View("Error");

                    var result = userManager.DeleteAsync(user).Result;
                    if (!result.Succeeded)
                        _errList.AddRange(result.Errors);
                    else _success = true;
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