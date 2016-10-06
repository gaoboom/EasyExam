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

        public Response Add(Category newCate)
        {
            Response _resp = new Response();
            dbContext.Categories.Add(newCate);
            dbContext.SaveChanges();
            return _resp;
        }

        public Response Modify(Category Cate)
        {
            Response _resp = new Response();
            Category _cateToBeChanged = new Category();
            _cateToBeChanged=dbContext.Categories.Find(Cate.CategoryID);
            _cateToBeChanged = Cate;
            dbContext.SaveChanges();
            return _resp;
        }

        public Response Delete(Category deleCate)
        {
            Response _resp = new Response();
            dbContext.Categories.Remove(deleCate);
            dbContext.SaveChanges();
            return _resp;
        }

        public List<Category> GetCategoryAndSort()
        {
            List<Category> _cateList = new List<Category>();
            List<Category> _cateListSorted = new List<Category>();
            int _maxLevel = 0;
            //获取所有栏目数据
            _cateList = dbContext.Categories.ToList();
            //获取顶级栏目数据并按Order排序
            _cateListSorted.AddRange(from c in _cateList where c.Level==0 orderby c.Order select c);
            //获取最大子栏目级数
            _maxLevel = (from c in _cateList orderby c.Level select c.Level).Max();
            //开始按栏目级数进行遍历
            for(int i=1;i<=_maxLevel;i++)
            {
                //实例化当前级数栏目临时变量
                List<Category> _cateListLevelTemp = new List<Category>();
                for(int j=0;j<= _cateListSorted.Count-1;j++)
                {
                    _cateListLevelTemp = (from c in _cateList where c.Level==i && c.ParentID== _cateListSorted[j].CategoryID orderby c.Order select c).ToList();
                    _cateListSorted.InsertRange(j+1, _cateListLevelTemp);
                }
            }

            return _cateListSorted;
        }
    }
}