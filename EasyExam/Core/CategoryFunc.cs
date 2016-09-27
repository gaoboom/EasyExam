using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyExam.Core
{
    /// <summary>
    /// 栏目操作功能类
    /// <remarks>
    /// Created at 2016.09.26
    /// </remarks>
    /// </summary>
    public class CategoryFunc
    {
        private EasyExamContext dbContext = new EasyExamContext();

        /// <summary>
        /// 根据栏目名称获取已有的栏目实例
        /// </summary>
        /// <param name="cateName">栏目名称</param>
        /// <returns>栏目实例</returns>
        public Category Find(string cateName)
        {
            var _cate = dbContext.Categories.First(c => c.Name == cateName);
            return _cate;
        }

        /// <summary>
        /// 根据栏目ID获取已有栏目实例
        /// </summary>
        /// <param name="cateID">栏目ID</param>
        /// <returns>栏目实例</returns>
        public Category Find(int cateID)
        {
            var _cate = dbContext.Categories.Find(cateID);
            return _cate;
        }

        public Response Add(string catName,int parentID=0,string description="",int order=50)
        {
            Response _resp = new Response();
            Category newCate = new Category();
            newCate.Name = catName;
            newCate.ParentID = parentID;
            newCate.Description = description;
            newCate.Order = order;
            dbContext.Categories.Add(newCate);
            dbContext.SaveChanges();
            return _resp;
        }

        public List<Category> GetCategoryAndSort()
        {
            List<Category> _cateList = new List<Category>();

            _cateList = dbContext.Categories.ToList();
            for(int i=0;i<=_cateList.Count-1;i++)
            {
                if(_cateList[i].ParentID!=0)
                {
                    _cateList.Add(_cateList[i]);
                    _cateList.Remove(_cateList[i]);
                }
            }
            return _cateList;
        }
    }
}