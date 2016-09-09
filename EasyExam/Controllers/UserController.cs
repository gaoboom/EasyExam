using EasyExam.Core;
using EasyExam.ViewModel;
using EasyExam.EXT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace EasyExam.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [UserAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                UserFunc userFunc = new UserFunc();
                var _response = userFunc.Verify(userLoginViewModel.Username, userLoginViewModel.Password);
                if(_response.Code==1)
                {
                    var _user = userFunc.Find(userLoginViewModel.Username);
                    Session.Add("UserID", _user.UserID);
                    Session.Add("Username", _user.Username);
                    userFunc.UpdateUserLoginInfo(_user.UserID, DateTime.Now,Request.ServerVariables["REMOTE_ADDR"]);
                    return RedirectToAction("Index", "User");
                }
                else if(_response.Code == 3)
                {
                    ModelState.AddModelError("Username Or Password", _response.Message);
                }
                else
                {
                    ModelState.AddModelError("", _response.Message);
                }
            }
            return View(userLoginViewModel);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}