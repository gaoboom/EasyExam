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
            CategoryAddViewModel _newCate = new CategoryAddViewModel();
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
                _newCate.PName = "无";
                _newCate.ParentID = 0;
                _newCate.Order = 50;
                _newCate.Level = 0;
            }
            else
            {
                _pcate = cf.Find(pid);
                _newCate.PName = _pcate.Name;
                _newCate.ParentID = _pcate.CategoryID;
                _newCate.Order = 50;
                _newCate.Level = _pcate.Level + 1;
            }
            ViewBag.Title = "添加栏目";
            return View(_newCate);
        }

        [AdminAuthorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateCategory(CategoryAddViewModel newCateVM)
        {
            if(ModelState.IsValid)
            {
                CategoryFunc cf = new CategoryFunc();
                Category newCate = new Category();
                newCate.Name = newCateVM.Name;
                newCate.ParentID = newCateVM.ParentID;
                newCate.Description = newCateVM.Description;
                newCate.Order = newCateVM.Order;
                newCate.Level = newCateVM.Level;
                cf.Add(newCate);
                return RedirectToAction("Category", "Admin");
            }
            else
            {
                return View(newCateVM);
            }
        }

        /// <summary>
        /// 修改栏目get
        /// </summary>
        /// <param name="id">栏目ID，如果为空，返回栏目首页</param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult ModifyCategory(int? id)
        {
            ViewBag.Title = "修改栏目";
            CategoryFunc cf = new CategoryFunc();
            Category _cate = new Category();
            if (id==null || id.GetType() != typeof(int))
            {
                return RedirectToAction("Category", "Admin");
            }
            else if(cf.Find((int)id)==null)
            {
                return RedirectToAction("Category", "Admin");
            }
            else
            {
                _cate = cf.Find((int)id);
                CategoryAddViewModel _cateVM = new CategoryAddViewModel();
                if(_cate.ParentID==0)
                {
                    _cateVM.PName = "无";
                }
                else
                {
                    _cateVM.PName = cf.Find(_cate.ParentID).Name;
                }
                
                _cateVM.ParentID = _cate.ParentID;
                _cateVM.Name = _cate.Name;
                _cateVM.Description = _cate.Description;
                _cateVM.Order = _cate.Order;
                _cateVM.Level = _cate.Level;
                ViewBag.CategoryID = _cate.CategoryID;
                return View(_cateVM);
            }
        }

        /// <summary>
        /// 修改栏目post
        /// </summary>
        /// <param name="cateVM">栏目ViewModel</param>
        /// <returns></returns>
        [AdminAuthorize,HttpPost,ValidateAntiForgeryToken]
        public ActionResult ModifyCategory(CategoryAddViewModel cateVM,int categoryID)
        {
            ViewBag.Title = "修改栏目";
            CategoryFunc cf = new CategoryFunc();
            Category _cate = new Category();
            if(ModelState.IsValid)
            {
                _cate.CategoryID = categoryID;
                _cate.Name = cateVM.Name;
                _cate.ParentID = cateVM.ParentID;
                _cate.Description = cateVM.Description;
                _cate.Order = cateVM.Order;
                _cate.Level = cateVM.Level;
                cf.Modify(_cate);
                return RedirectToAction("Category", "Admin");
            }
            else
            {
                return View(cateVM);
            }
        }
    }
}