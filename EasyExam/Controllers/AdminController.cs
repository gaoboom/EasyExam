using EasyExam.Core;
using EasyExam.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyExam.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [AdminAuthorize]
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
        public ActionResult Login(AdminLoginViewModel adminLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                AdminFunc adminFunc = new AdminFunc();
                var _response = adminFunc.Verify(adminLoginViewModel.Accounts, adminLoginViewModel.Password);
                if (_response.Code == 1)
                {
                    var _user = adminFunc.Find(adminLoginViewModel.Accounts);
                    Session.Add("AdminID", _user.AdministratorID);
                    Session.Add("Accounts", _user.Accounts);
                    adminFunc.UpdateAdminLoginInfo(_user.AdministratorID, DateTime.Now, "127.0.0.1");
                    return RedirectToAction("Index", "Admin");
                }
                else if (_response.Code == 3)
                {
                    ModelState.AddModelError("Accounts Or Password", _response.Message);
                }
                else
                {
                    ModelState.AddModelError("", _response.Message);
                }
            }
            return View(adminLoginViewModel);
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

        /// <summary>
        /// 栏目列表页
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Category()
        {
            CategoryFunc cf = new CategoryFunc();
            cf.GetCategoryAndSort();
            return View();
        }
    }
}