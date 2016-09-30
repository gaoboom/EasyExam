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
            List<Category> ctList = new List<Category>();
            ctList=cf.GetCategoryAndSort();
            ViewBag.Title = "栏目管理";
            return View(ctList);
        }

        /// <summary>
        /// 创建栏目Get
        /// </summary>
        /// <param name="id">父栏目ID，如果为空，设置为0</param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult CreateCategory(int? id)
        {
            Category _pcate = new Category();
            CategoryFunc cf = new CategoryFunc();
            int pid = 0;
            //当参数id为空时，表示创建根栏目，父栏目id设为0
            if (id==null)
            {
                pid = 0;
            }
            //当参数id不为整数时，强制设定父栏目id为0
            else if(id.GetType()!=typeof(int))
            {
                pid = 0;
            }
            else
            {
                pid = (int)id;
            }
            if(cf.Find(pid) == null)
            {
                _pcate.CategoryID = 0;
                _pcate.Name = "根栏目";
                _pcate.ParentID = -1;
                _pcate.Level = -1;
            }
            else
            {
                _pcate = cf.Find(pid);
            }
            ViewBag.Title = "创建栏目";
            return View(_pcate);
        }
    }
}