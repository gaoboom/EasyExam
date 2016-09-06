using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    public class UserFunc
    {
        private EasyExamContext dbContext = new EasyExamContext();

        public Response Verify(string username, string password)
        {
            Response _resp = new Response();
            var _user = dbContext.Users.Where(u => u.Username == username && u.Password==password);
            if(_user.Count()>0)
            {
                _resp.Code = 1;
                _resp.Message = "验证通过";
            }
            else
            {
                _resp.Code = 3;
                _resp.Message = "帐号或密码错误";
            }
            return _resp;
        }
    }
}