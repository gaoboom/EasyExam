using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 管理员操作功能类
    /// <remarks>
    /// Created at 2016.09.18
    /// </remarks>
    /// </summary>
    public class AdminFunc
    {
        private EasyExamContext dbContext = new EasyExamContext();

        public Response Verify(string accounts, string password)
        {
            Response _resp = new Response();
            var _admin = dbContext.Administrators.Where(u => u.Accounts == accounts && u.Password == password);
            if(_admin.Count()>0)
            {
                _resp.Code = 1;
                _resp.Message = "验证通过";
            }
            else
            {
                _resp.Code = 3;
                _resp.Message = "管理员帐号或密码错误";
            }
            return _resp;
        }

        public Response UpdateAdminLoginInfo(int adminID, DateTime dt, string ip)
        {
            Response _resp = new Response();
            var _admin = dbContext.Administrators.Find(adminID);
            if (_admin == null)
            {
                _resp.Code = 0;
                _resp.Message = "该主键的管理员不存在";
            }
            else
            {
                _admin.LoginIP = ip;
                _admin.LoginTime = dt;
                dbContext.SaveChanges();
            }
            return _resp;
        }

        public Administrator Find(string accounts)
        {
            var _admin = dbContext.Administrators.First(u => u.Accounts == accounts);
            return _admin;
        }

    }
}